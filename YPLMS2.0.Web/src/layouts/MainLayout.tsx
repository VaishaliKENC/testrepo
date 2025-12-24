import React, { useState, useEffect } from 'react';
import { useLocation } from 'react-router-dom';
import AppNavbar from '../components/Navigation/Navbar/Navbar';
import Sidebar from '../components/Navigation/Sidebar/Sidebar';
import Footer from '../components/Navigation/Footer/Footer';
import { useSelector } from 'react-redux';
import { RootState } from '../redux/store';

interface MainLayoutProps {
  children: React.ReactNode;
}

const MainLayout: React.FC<MainLayoutProps> = ({ children }) => {

  const userTypeId: any = useSelector((state: RootState) => state.auth.userTypeId);
  
  // Add an id identifier to identify learner side pages 
  // so that we can apply css without conflicting with admin side pages
  useEffect(() => {    
    if(userTypeId === "Admin") {
      document.body.id = 'yp-admin-pages';
      require("../styles/admin.scss");
    }
    else if(userTypeId === "Learner") {
      document.body.id = 'yp-learner-pages';

      require("../styles/learner.scss");
      require("../styles/learner-theme.scss");
    } else {
      document.body.removeAttribute("id");
    }
    
    return() => {
      // full page reload
      // forceful page refresh to append and remove learner css 
      // if we do not do this, then learner css is not removed when user jumps to admin page 
      window.location.reload(); 
    }
  }, [userTypeId]);

  // State to manage sidebar visibility
  const [isSidebarOpen, setIsSidebarOpen] = useState(true);
  // Function to toggle the sidebar state
  const toggleSidebar = () => {
    setIsSidebarOpen((prevState) => !prevState);
  };  
  
  const location = useLocation();
  useEffect(() => {
    setIsSidebarOpen(true);
  }, [location]);

  // useEffect to set sidebar open on initial load
  useEffect(() => {
    setIsSidebarOpen(true);
  }, []); // Empty dependency array means this runs only once on mount


  // Close sidebar on document/body click   
  useEffect(() => {
    const handleClick = (event: MouseEvent) => {
      let windowWidth = window.innerWidth; 
      const sidebarElement = document.querySelector('.yp-sidebar') as HTMLElement | null;
      if (
        isSidebarOpen &&
        sidebarElement &&
        !sidebarElement.contains(event.target as Node)
        && windowWidth < 1024
      ) {
        setIsSidebarOpen(false);
      }
    };

    document.addEventListener('click', handleClick);
    return () => {
      document.removeEventListener('click', handleClick);
    };
  }, [isSidebarOpen]);

  // Sidebar active menu's submenu show on page load
  // trigger via code to work data-bs-auto-close event
  useEffect(() => {
    setTimeout(() => {      
      const activeDropdown = document.querySelector('.yp-sidebar-link.dropdown-toggle.active') as HTMLElement | null;
      if (activeDropdown) {
        if (activeDropdown.nextElementSibling?.classList.contains('show')) {
          return;
        }
        else {
          activeDropdown.click(); // Trigger the click
        }        
      }
    }, 300);
  }, [location]);

  // Set main content box height
  const setMainContentBoxHeight = () => {
    let windowHt = window.innerHeight;
    let topbarHt = document.getElementById("yp-top-navbar-section")?.clientHeight;
    let breadcrumbHt = document.getElementById("yp-page-title-breadcrumb-section")?.clientHeight;
    let footerHt = document.getElementById("yp-footer-container")?.clientHeight;
    let finalHt;
    let mainCardSectionHt = document.getElementById("yp-card-main-content-section");
    let mainCardSubSectionHt = document.getElementById("yp-tree-sidebar-table-wrapper");
    if(topbarHt && breadcrumbHt && footerHt && mainCardSectionHt) {
      finalHt = windowHt - (topbarHt + breadcrumbHt + footerHt + 20 + 50 + 30);
      mainCardSectionHt.style.minHeight = `${finalHt}px`;

      if(mainCardSubSectionHt) {
        // YPLS-560 issue: added 150px height because to give extra height for folder tree structure list to visible more nodes and fix the issue
        mainCardSectionHt.style.minHeight = `${finalHt + 150}px`;
        mainCardSubSectionHt.style.minHeight = `${finalHt + 150}px`;
      }
      if (document.body.classList.contains('modal-open')) {
        if(mainCardSubSectionHt) {
          let modalHeaderHt = document.getElementsByClassName("modal-header")[0]?.clientHeight;
          finalHt = windowHt - modalHeaderHt;
          mainCardSubSectionHt.style.minHeight = `${finalHt}px`;
        }
      } 
      
      // To fix issue YPLS-589: Revamp --> With 100% zoom of the screen, all functions for Configure Profile Definition not visible
      // Table vertical scroll (by setting max height) added specific to configure profile definition page only
      // let configureProfileTable = document.getElementById("yp-configure-profile-table");
      // if(configureProfileTable) {
      //   configureProfileTable.style.maxHeight = `${finalHt}px`;
      // }
    }
  }

  // Check if table has scroll and apply class
  // to apply shadow to action column inside table
  const checkScroll = () => {
    setTimeout(function() {
    const wrappers = document.querySelectorAll<HTMLDivElement>(
      ".table-responsive"
    );

    wrappers.forEach((wrapper) => {
      const table = wrapper.querySelector<HTMLTableElement>(
        ".yp-custom-table"
      );
      if (table) {
        if (table.scrollWidth > wrapper.clientWidth) {
          table.classList.add("yp-table-has-scroll");
        } else {
          table.classList.remove("yp-table-has-scroll");
        }
      }
    });
  }, 500);
  };

  useEffect(() => {
    setMainContentBoxHeight();
    checkScroll();
  }, [location, isSidebarOpen]);

  useEffect(() => {
    window.addEventListener("resize", setMainContentBoxHeight);
    window.addEventListener("resize", checkScroll);

    return () => {
      window.removeEventListener("resize", setMainContentBoxHeight);
      window.addEventListener("resize", checkScroll);
    };
  }, []);

  return (
    <div id='yp-page-container' className={`${isSidebarOpen ? "yp-sidebar-open" : "yp-sidebar-close"}`}>
      {/* Sidebar */}
      <Sidebar />

      {/* Main Content Area */}
      <div style={{ flex: 1, display: 'flex', flexDirection: 'column', width: '100%' }}>
        {/* Sticky Navbar */}
        <div id="yp-top-navbar-section" style={{ position: 'sticky', top: 0, zIndex: 1000 }}>
          <AppNavbar />
        </div>

        {/* Scrollable Main Content */}
        <main id='yp-main-container'>
          <button onClick={toggleSidebar} className="toggle-button" id="yp-btnToggleSidebar">
            {/* {isSidebarOpen ? "Close Sidebar" : "Open Sidebar"} */}
            <i className="fa fa-bars"></i>
          </button>

          {children}

          {/* Sticky Footer */}
          <div id='yp-footer-container'>
            <Footer />
          </div>
        </main>        
      </div>

       {/* Add the SessionTimeout component here */}
       {/* <SessionTimeout/> */}
    </div>
  )
};

export default MainLayout;
