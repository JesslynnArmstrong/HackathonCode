import React, {useState} from 'react';

import Progress0 from "../images/progress.results.png"
import Progress1 from "../images/progress.results.gift.png"
import Progress2 from "../images/progress.goals.png"
import Progress3 from "../images/progress.goals.popup.png"

function OtherMockups({onFinish}) {

    const [step, setStep] = useState(0)

    function renderMockup() {

        if(step === 5){
            onFinish()
        }

        switch (step) {
            case 0:
                return <img src={Progress0} alt={"mockup"}/>
            case 1:
                return <img src={Progress1} alt={"mockup"}/>
            case 2:
                return <img src={Progress0} alt={"mockup"}/>
            case 3:
                return <img src={Progress2} alt={"mockup"}/>
            case 4:
                return <img src={Progress3} alt={"mockup"}/>
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

export default OtherMockups;
