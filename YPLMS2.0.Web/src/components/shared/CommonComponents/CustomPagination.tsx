import { useEffect, useRef, useState } from "react";

interface CustomPaginationProps {
  totalRecords: number;
  currentPage: number;
  onPageChange: (page: number, pagesize: number) => void;
  pageSize: number;
  handlePageClick: (page: number) => void;
}
const CustomPagination = (params: CustomPaginationProps) => {
  const { totalRecords, currentPage, onPageChange, pageSize, handlePageClick } =
    params;
  const paginationRef = useRef<HTMLDivElement>(null);
  const [isSticky, setIsSticky] = useState(false);
  const totalPages = Math.ceil(totalRecords / pageSize);
  const pageNumbers = [];
  for (let i = 1; i <= totalPages; i++) {
    if (
      i === 1 ||
      i === totalPages ||
      (i >= currentPage - 2 && i <= currentPage + 2)
    ) {
      pageNumbers.push(i);
    }
  }
  const scrollContainer = document.getElementById("yp-main-container");

  const stickNavbar = () => {
    if (scrollContainer) {
      const distanceFromBottom =
        scrollContainer.scrollHeight -
        scrollContainer.scrollTop -
        scrollContainer.clientHeight;
      if (distanceFromBottom < 200) {
        setIsSticky(false);
      }
     else if (scrollContainer.scrollTop >= 50) {
        setIsSticky(true);
      } else {
        setIsSticky(true);
      }
    }
  };

  useEffect(() => {
    if (scrollContainer) {
      scrollContainer.addEventListener("scroll", stickNavbar);
    }
    return () => {
      scrollContainer?.removeEventListener("scroll", stickNavbar);
    };
  }, [scrollContainer]);

 
  return (
    <div
      className={`yp-custom-pagination d-flex align-items-center flex-wrap ${
        isSticky ? "sticky" : ""
      }`}
      ref={paginationRef}
    >
      {/* pagination */}
      {totalRecords > 0 ? (
        <ul className="pagination">
          <li
            className={`page-item page-item-prev ${
              currentPage === 1 ? "disabled" : ""
            }`}
          >
            <a
              className="page-link"
              onClick={() => onPageChange(currentPage - 1, pageSize)}
            >
              <i className="fa fa-angle-left"></i>
            </a>
          </li>
          {pageNumbers.map((page) => (
            <li
              key={page}
              className={`page-item ${currentPage === page ? "active" : ""}`}
            >
              <a className="page-link" onClick={() => handlePageClick(page)}>
                {page}
              </a>
            </li>
          ))}
          <li
            className={`page-item page-item-next ${
              currentPage === totalPages ? "disabled" : ""
            }`}
          >
            <a
              className="page-link"
              onClick={() => onPageChange(currentPage + 1, pageSize)}
            >
              <i className="fa fa-angle-right"></i>
            </a>
          </li>
        </ul>
      ) : (
        <></>
      )}
    </div>
  );
};

export default CustomPagination;
