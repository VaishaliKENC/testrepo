const AssignedCourseIcon: React.FC<React.SVGProps<SVGSVGElement>> = (
  props
) => {
  return (
    <svg
      width="32"
      height="28"
      viewBox="0 0 25 23"
      fill="none"
      xmlns="http://www.w3.org/2000/svg"
      {...props}
    >
      <mask id="path-1-inside-1_1_98" fill="white">
        <rect y="0.5" width="25" height="18.8571" rx="2" />
      </mask>
      <rect
        y="0.5"
        width="25"
        height="18.8571"
        rx="2"
        stroke="white"
        strokeWidth="5"
        mask="url(#path-1-inside-1_1_98)"
      />
      <rect
        x="7.69232"
        y="19.3571"
        width="9.61539"
        height="3.14286"
        fill="white"
      />
      <rect
        x="4.80768"
        y="12.0238"
        width="15.3846"
        height="2.09524"
        fill="white"
      />
      <rect
        x="4.80768"
        y="6.78571"
        width="15.3846"
        height="2.09524"
        fill="white"
      />
    </svg>
  );
};
export default AssignedCourseIcon;
