import { FC, useEffect, useState } from "react";
import { useCombobox, autocomplete } from "@szhsin/react-autocomplete";

export interface AutocompleteProps {
  items: { label: string; value: string }[];
  placeholder: string;
  title?: string;
  value?: { label: string; value: string } | null;
  handleSelectChange: (selectedOption: { label: string; value: string } | null) => void;
  onSearch?: (keyword: string) => void;
}

const AutoAsyncSelect: FC<AutocompleteProps> = ({
  items,
  placeholder,
  title = "Type to search...",
  value: controlledValue,
  handleSelectChange,
  onSearch,
}) => {
  const [inputValue, setInputValue] = useState("");

  // Sync input with selected value from parent
//   useEffect(() => {
//     if (controlledValue) {
//       setInputValue(controlledValue.label);
//     } else {
//       setInputValue("");
//     }
//   }, [controlledValue]);
useEffect(() => {
  if (controlledValue && controlledValue.label !== inputValue) {
    setInputValue(controlledValue.label);
  }
  if (!controlledValue && inputValue !== "") {
    setInputValue("");
  }
}, [controlledValue]);


  const handleChange = (val?: string) => {
    setInputValue(val || "");
    onSearch?.(val || "");
  };

  const handleSelection = (item?: { label: string; value: string }) => {
    handleSelectChange(item || null);
  };

  const {
    getInputProps,
    getClearProps,
    getListProps,
    getItemProps,
    open,
    isInputEmpty,
  } = useCombobox({
    items,
    getItemValue: (item) => item.label,
    value: inputValue,
    onChange: handleChange,
    onSelectChange: handleSelection,
    feature: autocomplete({ select: true }),
  });

  return (
    <div className={`yp-form-control-autocomplete-new ${open ? "open" : ""}`}>
      <div className="yp-form-control-wrapper">
        <input
          type="text"
          {...getInputProps()}
          onBlur={() => {
            // Reset input to selected value on blur
            if (controlledValue) setInputValue(controlledValue.label);
          }}
          placeholder=""
          title={title}
          className="form-control yp-form-control"
          onKeyDown={(e) => {
            if (e.key === "Enter") {
              e.preventDefault();
              // ✅ If items exist, select first item on Enter
              if (items.length > 0) handleSelection(items[0]);
            }
          }}
        />
        <label className="form-label">{placeholder}</label>
        <span className="yp-form-control-icon">
          <i className="fa fa-search"></i>
        </span>
        {!isInputEmpty && (
          <span
            className="yp-form-control-icon yp-form-control-icon-right-side"
            // {...getClearProps()}
             {...getClearProps()}
           onClick={() => handleSelectChange(null)}
          >
            <i className="fa fa-close"></i>
          </span>
        )}
      </div>

      <ul
        className="yp-autocomplete-list"
        {...getListProps()}
        style={{ display: open ? "block" : "none" }}
      >
        {items.length ? (
          items.map((item, index) => (
            <li key={item.value} {...getItemProps({ item, index })}>
              {item.label}
            </li>
          ))
        ) : (
          <li>No Data Found</li>
        )}
      </ul>
    </div>
  );
};

export default AutoAsyncSelect;
