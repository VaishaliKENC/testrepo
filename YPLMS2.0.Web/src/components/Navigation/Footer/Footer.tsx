import React from "react";
import encLogo from '../../../assets/images/enc_logo 1.png';
import ypLogo from '../../../assets/images/YPLMS_logo.jpg';
import "./Footer.css";

const Footer: React.FC = () => {
  return (
    <footer className="footer">
      <div className="row column-gap-2 row-gap-3 gap-md-0 align-items-center">
        {/* Left Section */}
        <div className="col-md-2">
          <div className="yp-footer-logo-wrapper d-flex align-items-center">
              <img src={ypLogo} alt="YPLMS Logo" height="35" />
              {/* <div className="yp-footer-text-powered">
                <span className="yp-footer-text-by">Powered by{" "}</span>
                <span className="yp-text-orange-ypls">Yellow Platter Learning Suite</span>
              </div> */}
          </div>
        </div>

        {/* Right Section */}
        <div className="col-md-10 text-md-end">
          <div className="yp-footer-links-copyright-wrapper d-flex align-items-md-center align-items-start justify-content-md-end flex-md-row flex-column column-gap-4 row-gap-3">
            <div className="yp-footer-links-wrapper d-inline-flex">
              <ul className="yp-footer-links-inner-pages">
                <li><a href="">Privacy Policy</a></li>
                <li>|</li>
                <li><a href="">Terms of Use</a></li>
                <li>|</li>
                <li><a href="">Purchase Policy</a></li>
                <li>|</li>
                <li><a href="">About Us</a></li>
              </ul>
            </div>
            <div className="yp-footer-copyright-wrapper d-inline-flex align-items-center gap-2">
              <span className="yp-footer-text-year">2025 &copy;</span>
              <img
                height="22"
                src={encLogo}
                alt="Encora Logo"
              />
            </div>
          </div>
        </div>
      </div>
    </footer>
  );
};

export default Footer;
