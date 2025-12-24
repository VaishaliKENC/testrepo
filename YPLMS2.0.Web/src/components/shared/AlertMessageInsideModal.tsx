import React, { useEffect, useState } from "react";
import { parse } from "path";

interface AlertProps {
  type: "success" | "error" | "info";
  message: string;
  duration?: string;
  autoClose?: boolean;
  onClose?: () => void;
}

const AlertMessageInsideModal: React.FC<AlertProps> = ({ type, message, duration="4000", autoClose = true, onClose }) => {
  const [visible, setVisible] = useState(true);
  const [isAnimatingOut, setIsAnimatingOut] = useState(false);

  useEffect(() => {
    if (!autoClose) return;    

    const timer = setTimeout(() => {
      setIsAnimatingOut(true);
    }, parseInt(duration));
    return () => clearTimeout(timer);
  }, [autoClose, duration]);

  const handleAnimationEnd = () => {
    if (isAnimatingOut) {
      setVisible(false);
      if (onClose) {
        onClose();
      }
    }
  };

  const handleClose = () => {
    setIsAnimatingOut(true);
  };
  
  if (!visible) return null;

  // Alert settings based on type
  const alertConfig = {
    success: { icon: <i className="fa fa-check-circle"></i>, className: "alert-success" },
    error: { icon: <i className="fa fa-warning"></i>, className: "alert-error" },
    info: { icon: <i className="zmdi zmdi-info-outline"></i>, className: "alert-light" },
  };

  // Positioning Classes
  const positionClasses = {
    "top-left": "justify-content-start align-items-start",
    "top-right": "justify-content-start align-items-end",    
    "top-center": "justify-content-start align-items-center",
    "bottom-left": "justify-content-end align-items-start",
    "bottom-right": "justify-content-end align-items-end",
    "bottom-center": "justify-content-end align-items-center",
    "center": "justify-content-center align-items-center",
  };

  const animationClasses = {
    "top-left": 'slide-in-top-left',
    "top-right": 'slide-in-top-right',
    "top-center": 'slide-in-top',
    "bottom-left": 'slide-in-bottom-left',
    "bottom-right": 'slide-in-bottom-right',
    "bottom-center": 'slide-in-bottom',
    "center": 'slide-in-center',
  };

  return (
    <div className={`yp-alert-message yp-alert-message-modal`}>      
      <div className={`alert ${alertConfig[type].className}`} 
      role="alert">
        <div className="yp-alert-icon">
          {alertConfig[type].icon}
        </div>
        <div className="yp-alert-content">
          {message} 
        </div>
      </div>
    </div>
  );
};

export default AlertMessageInsideModal;
