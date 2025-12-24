import { StatisticsCardsProps } from "./StatisticsCards.type";

const StatisticsCards = ({ data }: { data: StatisticsCardsProps }) => {
  const { count, label, icon, bgClass, iconBgClass } = data;
  return (
    <div className="col-md-41 col-sm-61">
      <div className={`yp-learner-dashboard-stat-box ${bgClass}`}>
        <div className="yp-learner-dsb-icon-wrapper">
          <span className={`yp-learner-dsb-icon-circle ${iconBgClass}`}>
            {icon}
          </span>
        </div>
        <div className="yp-learner-dsb-info-wrapper">
          <div className="yp-learner-dsb-info-count">
            {count !== 0 ? count.toString().padStart(2, "0") : count}
          </div>
          <div className="yp-learner-dsb-info-text">{label}</div>
        </div>
      </div>
    </div>
  );
};

export default StatisticsCards;
