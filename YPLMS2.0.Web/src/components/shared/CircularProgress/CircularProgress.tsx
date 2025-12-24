import { useEffect, useState } from "react";

const CircularProgress = ({ progress }: { progress: number }) => {

  const [animatedProgress, setAnimatedProgress] = useState(0);
  useEffect(() => {
    // Delay the update slightly to trigger the CSS transition
    const timeout = setTimeout(() => {
      setAnimatedProgress(progress);
    }, 100);

    return () => clearTimeout(timeout);
  }, [progress]);
  
  return (
    <div className="yp-circle-progress">
      <svg viewBox="0 0 36 36" className="yp-circle-progress-chart">
        <path
          className="yp-circle-progress-bg"
          d="M18 2

                                            a 16 16 0 0 1 0 32

                                            a 16 16 0 0 1 0 -32"
        />
        <path
          className="yp-circle-progress-progress"
          strokeDasharray={`${animatedProgress}, 100`}
          d={animatedProgress > 0 ? "M18 2 a 16 16 0 0 1 0 32 a 16 16 0 0 1 0 -32" : "" }
        />
        <text x="18" y="22" className="yp-circle-progress-percentage">
          {progress}%
        </text>
      </svg>
    </div>
  );
};
export default CircularProgress;
