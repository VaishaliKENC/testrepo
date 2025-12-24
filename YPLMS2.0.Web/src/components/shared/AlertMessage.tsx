import React, { useEffect, useRef, useState } from "react";
import { CheckCircle, AlertTriangle, XCircle } from "lucide-react";
import { parse } from "path";

interface AlertProps {
  type: "success" | "warning" | "error";
  message: string;
  position?: "top-left" | "top-right" | "bottom-left" | "bottom-right" | "top-center" | "bottom-center" | "center";
  duration?: string;
  autoClose?: boolean;
  onClose?: () => void;
}

const AlertMessage: React.FC<AlertProps> = ({ type, message, position = "bottom-center", duration="4000", autoClose = true, onClose }) => {
  const [visible, setVisible] = useState(true);
  const [isAnimatingOut, setIsAnimatingOut] = useState(false);

  const [isPaused, setIsPaused] = useState(false);
  const [progress, setProgress] = useState(100); // NEW
  const intervalRef = useRef<NodeJS.Timeout | null>(null);
  const startTimeRef = useRef<number>(Date.now());
  const remainingTimeRef = useRef(parseInt(duration));

  // Progress bar logic with pause/play
  useEffect(() => {
    if (!autoClose) return;

    const updateProgress = () => {
      const elapsed = Date.now() - startTimeRef.current;
      const remaining = remainingTimeRef.current - elapsed;

      if (remaining <= 0) {
        setProgress(0);
        setIsAnimatingOut(true);
        clearInterval(intervalRef.current!);
      } else {
        const percent = (remaining / parseInt(duration)) * 100;
        setProgress(percent);
      }
    };

    if (!isPaused) {
      startTimeRef.current = Date.now();
      intervalRef.current = setInterval(updateProgress, 100);
    }

    return () => {
      if (intervalRef.current) clearInterval(intervalRef.current);
    };
  }, [isPaused, autoClose, duration]);

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

  const handleMouseEnter = () => {
    if (autoClose && !isPaused) {
      const elapsed = Date.now() - startTimeRef.current;
      remainingTimeRef.current -= elapsed;
      setIsPaused(true);
    }
  };

  const handleMouseLeave = () => {
    if (autoClose && isPaused) {
      setIsPaused(false);
    }
  };

  const togglePausePlay = () => {
    if (isPaused) {
      setIsPaused(false);
    } else {
      const elapsed = Date.now() - startTimeRef.current;
      remainingTimeRef.current -= elapsed;
      setIsPaused(true);
    }
  };
  
  if (!visible) return null;

  // Alert settings based on type
  const alertConfig = {
    success: { icon: <i className="fa fa-check-circle"></i>, className: "alert-success" },
    warning: { icon: <i className="fa fa-warning"></i>, className: "alert-warning" },
    error: { icon: <i className="fa fa-exclamation-circle"></i>, className: "alert-error" },
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
    <div className={`yp-alert-message yp-alert-message-fixed ${positionClasses[position]}`}>      
      <div className={`alert fade show ${alertConfig[type].className} ${animationClasses[position]} ${isAnimatingOut ? 'slide-out' : ''}`} 
      role="alert" 
      onAnimationEnd={handleAnimationEnd}
      onMouseEnter={handleMouseEnter}
      onMouseLeave={handleMouseLeave}
      >
        <div className="yp-alert-fixed-icon">
          {alertConfig[type].icon}
        </div>
        <div className="yp-alert-fixed-content">
          <strong>{type.toUpperCase()}!</strong><br />
          {message}
        </div>
        <div className="yp-alert-fixed-close">
          {autoClose && (
            <span className="yp-alert-fixed-pauseplay" onClick={togglePausePlay}>
              {isPaused ? <i className="fa fa-play"></i> : <i className="fa fa-pause"></i>}
            </span>
          )}
          <button type="button" className="btn-close" onClick={handleClose} aria-label="Close"></button>
        </div>

        <div className="yp-alert-progress-bar"
          style={{
            width: `${progress}%`,
            transition: "width 0.1s linear",
          }}          
        ></div>
      </div>
    </div>
  );
};

export default AlertMessage;
