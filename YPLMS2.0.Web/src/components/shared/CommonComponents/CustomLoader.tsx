const CustomLoader = () => {
  return (
    <div className="yp-loading-overlay">
      <div className="yp-spinner">
        <div className="spinner-border" role="status">
          <span className="visually-hidden">Loading...</span>
        </div>
      </div>
    </div>
  );
};
export default CustomLoader;
