const CompletedCourseIcon: React.FC<React.SVGProps<SVGSVGElement>> = (
  props
) => {
  return (
    <svg
      width="32"
      height="28"
      viewBox="0 0 26 23"
      fill="none"
      xmlns="http://www.w3.org/2000/svg"
      {...props}
    >
      <mask id="path-1-inside-1_1_111" fill="white">
        <rect y="0.5" width="26" height="19.2957" rx="2" />
      </mask>
      <rect
        y="0.5"
        width="26"
        height="19.2957"
        rx="2"
        stroke="white"
        strokeWidth="5"
        mask="url(#path-1-inside-1_1_111)"
      />
      <rect x="7.50012" y="19.2841" width="10" height="3.21595" fill="white" />
      <path
        d="M7.11532 8.68604L11.266 13.413L9.99293 14.5635L6 9.70929L7.11532 8.68604Z"
        fill="white"
      />
      <path
        d="M9.05479 13.423L16.4954 6.63953L17.4884 7.78999L9.99293 14.5635L9.05479 13.423Z"
        fill="white"
      />
      <path
        d="M10.6153 8.68604L14.766 13.413L13.4929 14.5635L9.5 9.70929L10.6153 8.68604Z"
        fill="white"
      />
      <path
        d="M12.5548 13.423L19.9954 6.63953L20.9884 7.78999L13.4929 14.5635L12.5548 13.423Z"
        fill="white"
      />
    </svg>
  );
};
export default CompletedCourseIcon;
