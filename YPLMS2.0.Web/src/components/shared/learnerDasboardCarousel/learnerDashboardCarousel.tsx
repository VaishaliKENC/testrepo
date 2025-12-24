
import bannerImg from '../../../assets/images/user_icon 1.png'
const LearnerDashboardCarousel=()=>{
    return (
        <div
        id="carouselDashboardBanner"
        className="yp-carousel carousel carousel-dark slide"
        data-bs-ride="carousel"
      >
        <div className="carousel-indicators">
          <button
            type="button"
            data-bs-target="#carouselDashboardBanner"
            data-bs-slide-to="0"
            className="active"
            aria-current="true"
            aria-label="Slide 1"
          ></button>
          <button
            type="button"
            data-bs-target="#carouselDashboardBanner"
            data-bs-slide-to="1"
            aria-label="Slide 2"
          ></button>
          <button
            type="button"
            data-bs-target="#carouselDashboardBanner"
            data-bs-slide-to="2"
            aria-label="Slide 3"
          ></button>
        </div>
        <div className="carousel-inner">
          <div className="carousel-item active">
            <div className="carousel-caption">
              <div className="carousel-caption-info">
                <h5>1. LMS New Features Banner</h5>
                <p>Step into the future of learning with us</p>
              </div>
              <img src={bannerImg} />
            </div>
          </div>
          <div className="carousel-item">
            <img src="https:////images.ctfassets.net/yon5rraf34cy/2ACThRbPyqrFejtomKA1u9/f093e8e4bd87a8a2a0c8a558f517088d/Hero.png" />
          </div>
          <div className="carousel-item">
            <div className="carousel-caption">
              <div className="carousel-caption-info">
                <h5>3. LMS New Features Banner</h5>
                <p>Step into the future of learning with us</p>
              </div>
              <img src={bannerImg} />
            </div>
          </div>
        </div>
        <button
          className="carousel-control-prev"
          type="button"
          data-bs-target="#carouselDashboardBanner"
          data-bs-slide="prev"
        >
          <span
            className="carousel-control-prev-icon"
            aria-hidden="true"
          ><i className="fa-solid fa-chevron-left"></i></span>
          <span className="visually-hidden">Previous</span>
        </button>
        <button
          className="carousel-control-next"
          type="button"
          data-bs-target="#carouselDashboardBanner"
          data-bs-slide="next"
        >
          <span
            className="carousel-control-next-icon"
            aria-hidden="true"
          ><i className="fa-solid fa-chevron-right"></i></span>
          <span className="visually-hidden">Next</span>
        </button>
      </div>
    )
}

export default LearnerDashboardCarousel;