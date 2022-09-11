import React, { useState } from 'react';

import BannerImage from '../../images/navbar.center.png';

function Banner({setMockups}) {

    const [bannerOpen, setBannerOpen] = useState(true);


    return (
        <>
            { bannerOpen && (
                <div className="fixed bottom-0 right-0 w-full md:bottom-8 md:right-12 md:w-auto z-60">
                        <img className="drop-shadow-none" src={BannerImage} alt={"image"} onClick={setMockups}/>
                </div>
            )}
        </>
    );
}

export default Banner;
