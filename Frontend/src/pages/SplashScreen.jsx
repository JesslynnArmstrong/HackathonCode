import React from 'react';
import SplashImage from '../images/logo.png';
import {TailSpin} from "react-loader-spinner";


function SplashScreen({onClick}) {

    return (
        <div style={{position: "absolute", top: 0, bottom: 0, backgroundColor : "#FEFEFE", display: "flex", flexDirection: "column", alignItems: "center", justifyContent: "center"}} onClick={() => onClick()}>
            <div>
                <img src={SplashImage} alt={"SplashScreen"}/>
                <div style={{display: "flex", flexDirection: "column", alignItems: "center", justifyContent: "center"}}>
                    <TailSpin
                        height="80"
                        width="80"
                        color="#7CD1E3"
                        ariaLabel="tail-spin-loading"
                        radius="1"
                        wrapperStyle={{}}
                        wrapperClass=""
                        visible={true}
                    />
                </div>
            </div>
        </div>
    );
}

export default SplashScreen;
