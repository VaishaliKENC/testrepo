import { FC } from "react";
import Select, { components } from "react-select";

export interface LeaderBoardAutocompleteProps {
  options: any[];
  placeholder: string;
  defaultValue?:Record<string,any>
  handleSelectChange: (selectedOption:string) => void;
}
const leaderBoardAutocomplete: FC<LeaderBoardAutocompleteProps> = ({
  options,
  placeholder,
  defaultValue,
  handleSelectChange,
}) => {
  const IndicatorSeparator = () => null;
  const customStyles = {
    control: (base: any) => ({
      ...base,
      flexDirection: "row-reverse",
      borderRadius: "20px",
    }),
    clearIndicator: (base: any) => ({
      ...base,
      position: "absolute",
      right: 0,
    }),
     menu: (provided:any) => ({
    ...provided,
    zIndex: 9999, // Make this high enough to be above other elements
  }),
  };
  const customComponents = {
    DropdownIndicator: (props: any) => (
      <components.DropdownIndicator {...props}>
        <i className="fa fa-search"></i>
      </components.DropdownIndicator>
    ),
    IndicatorSeparator: IndicatorSeparator,
    // ValueContainer: ({ children, ...props }) => {
    //   return (
    //     <components.ValueContainer {...props}>
    //       <i className="fa fa-search"></i>
    //       {/* Add styling for spacing */}
    //       {children}
    //     </components.ValueContainer>
    //   );
    // },
    // Control: (props: any) => (
    //   <components.Control {...props}>
    //     <span className="yp-form-control-icon" style={{ margin: "12px" }}>
    //       <i className="fa fa-search"></i>
    //     </span>
    //     {props.children}
    //   </components.Control>
    // ),
  };
   const onChange = (selectedValue:any) => {
    if (handleSelectChange) {
      handleSelectChange(selectedValue);
    }
  };
  return (
    <div className="yp-autocomplete-control">
      <Select
        className="basic-single"
        classNamePrefix="select"
        isLoading={false}
        isClearable
        isRtl={false}
        isSearchable
        name="course"
        options={options}
        defaultValue={defaultValue}
        placeholder={placeholder}
        noOptionsMessage={() => "No Course Found"}
        components={customComponents}
        styles={customStyles}
        onChange={onChange}
      />
    </div>
  );
};
export default leaderBoardAutocomplete;
