import React from 'react';
import { Link } from 'react-router-dom';

interface BreadcrumbItem {
  label?: string;
  path?: string;
  iconClass?: string; 
}

interface BreadcrumbProps {
  items: BreadcrumbItem[];
}

const CustomBreadcrumb: React.FC<BreadcrumbProps> = ({ items }) => {
  return (
    <div className="yp-page-breadcrumb">
      <nav aria-label="breadcrumb">
        <ol className="breadcrumb">
          {items.map((item, index) => (
            <li
              key={index}
              className={`breadcrumb-item ${index === items.length - 1 ? 'active' : ''}`}
              aria-current={index === items.length - 1 ? 'page' : undefined}
            >
              {index > 0 && <i className="fa fa-angle-right"></i>}
              {item.path ? (
                <Link to={item.path}>
                  {item.iconClass ? <i className={item.iconClass}></i> : item.label}
                </Link>
              ) : (
                item.iconClass ? <i className={item.iconClass}></i> : item.label
              )}
            </li>
          ))}
        </ol>
      </nav>
    </div>
  );
};

export default CustomBreadcrumb;
