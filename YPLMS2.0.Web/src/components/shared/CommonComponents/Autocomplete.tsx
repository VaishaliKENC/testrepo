import { FC, useEffect, useRef, useState } from "react";
import { useCombobox, autocomplete } from "@szhsin/react-autocomplete";
export interface AutocompleteProps {
  items: any[];
  placeholder: string;
  title?: string;
  defaultValue?: any;
  value?: { label: string; value: string } | null;
  handleSelectChange: (selectedOption: string) => void;
}
const Autocomplete: FC<AutocompleteProps> = ({
  items,
  placeholder,
  title = "Type to search...",
  defaultValue,
  handleSelectChange,

}) => {
  const [value, setValue] = useState<string>();
  const [selected, setSelected] = useState<{ label: string; value: string }>();
  const hasInitializedDefault = useRef(false);

  const hiddenInputRef = useRef<HTMLInputElement | null>(null);

  const filteredItems = value
    ? items.filter((item: any) =>
      item.label.toLowerCase().startsWith(value.toLowerCase())
    )
    : items;
  useEffect(() => {
    //input was getting clear on deselecting
    if (defaultValue && !hasInitializedDefault.current) {
      setValue(defaultValue.label);
      setSelected(defaultValue);
      hasInitializedDefault.current = true;
    } else if (!defaultValue) {
      hasInitializedDefault.current = false;
    }
  }, [defaultValue]);

  const handleChange = (item?: any) => {
    setValue(item);
  };

  const handleSelection = (item?: any) => {
    if (item === undefined) {
      setValue("");
      setSelected(undefined);
      handleSelectChange(item ?? "");
    } else {
      handleSelectChange(item);
      setValue(item.label);
      setSelected(item);
    }
  };

  const {
    getInputProps,
    getClearProps,
    getListProps,
    getItemProps,
    isItemSelected,
    open,
    focusIndex,
    isInputEmpty,
  } = useCombobox({
    items,
    // When items are objects, you must specify how to retrieve the text value from the item.
    getItemValue: (item: any) => item.label,
    value,
    onChange: handleChange,
    selected,
    onSelectChange: handleSelection,
    feature: autocomplete({ select: true }),
  });
  return (
    <>
      <div className={`yp-form-control-autocomplete-new ${open ? 'open' : ''}`}>
        <div className="yp-form-control-wrapper">
          <input
            type="text"
            name="tableSearchInput"
            //placeholder={placeholder}
            placeholder=""
            title={title}
            className="form-control yp-form-control"
            {...getInputProps()}
          // onKeyDown={(e) => {
          //   if (e.key === "Enter") e.preventDefault(); // ✅ block refresh
          // }}
          />
          <label className="form-label">{placeholder}</label>
          <span className="yp-form-control-icon">
            <i className="fa fa-search"></i>
          </span>
          {!isInputEmpty && (
            <span
              className="yp-form-control-icon yp-form-control-icon-right-side"
              {...getClearProps()}
            >
              <i className="fa fa-close"></i>
            </span>
          )}
        </div>
        <ul
          className="yp-autocomplete-list"
          {...getListProps()}
          style={{
            display: open ? "block" : "none",
          }}
        >
          {/* {filteredItems.length ? (
            filteredItems.map((item: any, index: number) => {
              return (
                <li
                  className={`${selected?.label === item.label ? 'current' : ''}`}
                  key={item.abbr}
                  {...getItemProps({ item, index })}
                >
                  {item.label}
                </li>
              );
            })
          ) : (
            <li>No Data Found</li>
          )}
        </ul>
        <input ref={hiddenInputRef} style={{ position: "absolute", opacity: 0, pointerEvents: "none", height: "0px", width: "0px" }} />
      </div> */}
    {/* </> */}
     {filteredItems.length ? (
            filteredItems.map((item: any, index: number) => {
              return (
                <li
                  style={{
                    background: focusIndex === index ? "#ddd" : "none",
                    textDecoration: selected?.label === item.label ? "underline" : "none",
                  }}
                  key={item.abbr}
                  {...getItemProps({ item, index })}
                >
                  {item.label}
                </li>
              );
            })
          ) : (
            <li>No Course Found</li>
          )}
        </ul>
      </div>
    </>
  );
};

export default Autocomplete;