import React, {useState} from 'react';

import Profile0 from "../images/profile.claims.png"
import Profile1 from "../images/profile.results.png"
import Profile2 from "../images/profile.profile1.png"
import Profile3 from "../images/profile.profile2.png"
import Profile4 from "../images/profile.profile3.png"


function Mockups({onFinish}) {

    const [step, setStep] = useState(0)

    function renderMockup() {

        if(step === 5){
            onFinish()
        }

        switch (step) {
            case 0:
                return <img src={Profile0} alt={"mockup"}/>
            case 1:
                return <img src={Profile1} alt={"mockup"}/>
            case 2:
                return <img src={Profile2} alt={"mockup"}/>
            case 3:
                return <img src={Profile3} alt={"mockup"}/>
            case 4:
                return <img src={Profile4} alt={"mockup"}/>
        }

    }

    return (
        <div style={{
            position: "absolute",
            top: -70,
            bottom: 0,
            backgroundColor: "#FEFEFE",
            display: "flex",
            flexDirection: "column",
            alignItems: "center",
            justifyContent: "center"
        }}>
            <div onClick={() => setStep(step + 1)}>
                {renderMockup()}
            </div>
        </div>
    );
}

export default Mockups;
