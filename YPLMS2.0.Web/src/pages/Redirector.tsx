import { useEffect } from "react";
import { useNavigate, useLocation } from "react-router-dom";

const Redirector: React.FC = () => {
    const navigate = useNavigate();
    const location = useLocation();

    const queryParams = new URLSearchParams(location.search);
    const target = queryParams.get("to");

    useEffect(() => {
        if (target) {
            navigate(target);
        } else {
            console.warn("Redirector: 'to' parameter not found.");
        }
    }, [target, navigate]);

    return (
        <div className="d-flex justify-content-center align-items-center" style={{ height: "100vh" }}>
          <div className="spinner-border text-primary" role="status">
            <span className="visually-hidden">Redirecting...</span>
          </div>
        </div>
    );
};

export default Redirector;