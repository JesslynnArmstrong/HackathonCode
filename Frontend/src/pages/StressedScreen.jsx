import React, {useState} from 'react';
import SplashImage from '../images/character.png';
import SportsSoccerIcon from '@mui/icons-material/SportsSoccer';
import WorkOutlineIcon from '@mui/icons-material/WorkOutline';
import MoreHorizIcon from '@mui/icons-material/MoreHoriz';
import RestaurantIcon from '@mui/icons-material/Restaurant';
import SentimentVeryDissatisfiedIcon from '@mui/icons-material/SentimentVeryDissatisfied';
import ExitToAppIcon from '@mui/icons-material/ExitToApp';
import ThumbDownOffAltIcon from '@mui/icons-material/ThumbDownOffAlt';
import QuestionMarkIcon from '@mui/icons-material/QuestionMark';
import ThumbUpOffAltIcon from '@mui/icons-material/ThumbUpOffAlt';


function StressedScreen({onFinish}) {

    const [stepIndex, setStepIndex] = useState(0)
    let text = ""

    function renderStep() {
        switch (stepIndex) {
            case 0:
                return (
                    <div style={{textAlign: "center"}}>
                        <br/>
                        <h4>Jenny, please tell us why you are stressed?</h4>
                        <br/>
                        <br/>
                        <div style={{
                            display: "flex",
                            flexDirection: "row",
                            alignItems: "center",
                            justifyContent: "center",
                            gap: 15
                        }}>
                            <button onClick={() => {setStepIndex(1)}}
                                className="bg-transparent font-semibold hover:text-white py-2 px-4 border hover:border-transparent rounded"
                                style={{borderColor: "#4FC1E0FF", color: "#4FC1E0FF", width: 100, height: 100}}>
                                <SportsSoccerIcon/>
                                <div>
                                    Sports
                                </div>
                            </button>
                            <button onClick={() => {setStepIndex(1)}}
                                className="bg-transparent font-semibold hover:text-white py-2 px-4 border hover:border-transparent rounded"
                                style={{borderColor: "#4FC1E0FF", color: "#4FC1E0FF", width: 100, height: 100}}>
                                <WorkOutlineIcon/>
                                <div>
                                    Work
                                </div>
                            </button>
                            <button onClick={() => {setStepIndex(1)}}
                                className="bg-transparent font-semibold hover:text-white py-2 px-4 border hover:border-transparent rounded"
                                style={{borderColor: "#4FC1E0FF", color: "#4FC1E0FF", width: 100, height: 100}}>
                                <ExitToAppIcon/>
                                <div>
                                    Moving
                                </div>
                            </button>
                        </div>
                        <br/>
                        <div style={{
                            display: "flex",
                            flexDirection: "row",
                            alignItems: "center",
                            justifyContent: "center",
                            gap: 15
                        }}>
                            <button onClick={() => {setStepIndex(1)}}
                                className="bg-transparent font-semibold hover:text-white py-2 px-4 border hover:border-transparent rounded"
                                style={{borderColor: "#4FC1E0FF", color: "#4FC1E0FF", width: 100, height: 100}}>
                                <SentimentVeryDissatisfiedIcon/>
                                <div>
                                    Emotion
                                </div>
                            </button>
                            <button onClick={() => {setStepIndex(1)}}
                                className="bg-transparent font-semibold hover:text-white py-2 px-4 border hover:border-transparent rounded"
                                style={{borderColor: "#4FC1E0FF", color: "#4FC1E0FF", width: 100, height: 100}}>
                                <RestaurantIcon/>
                                <div>
                                    Food
                                </div>
                            </button>
                            <button onClick={() => {setStepIndex(1)}}
                                className="bg-transparent font-semibold hover:text-white py-2 px-4 border hover:border-transparent rounded"
                                style={{borderColor: "#4FC1E0FF", color: "#4FC1E0FF", width: 100, height: 100}}>
                                <MoreHorizIcon/>
                                <div>
                                    Other
                                </div>
                            </button>
                        </div>
                    </div>
                )
            case 1:
                return (
                    <div style={{textAlign: "center"}}>
                        <br/>
                        <h4>And how do you feel right now</h4>
                        <br/>
                        <br/>
                        <div style={{
                            display: "flex",
                            flexDirection: "row",
                            alignItems: "center",
                            justifyContent: "center",
                            gap: 15
                        }}>
                            <button onClick={() => setStepIndex(2)}
                                    className="bg-transparent font-semibold hover:text-white py-2 px-4 border hover:border-transparent rounded"
                                    style={{borderColor: "#4FC1E0FF", color: "#4FC1E0FF", width: 100, height: 100}}>
                                <ThumbDownOffAltIcon/>
                                <div>
                                    Negative
                                </div>
                            </button>
                            <button onClick={() => setStepIndex(2)}
                                    className="bg-transparent font-semibold hover:text-white py-2 px-4 border hover:border-transparent rounded"
                                    style={{borderColor: "#4FC1E0FF", color: "#4FC1E0FF", width: 100, height: 100}}>
                                <QuestionMarkIcon/>
                                <div>
                                    Not sure
                                </div>
                            </button>
                            <button onClick={() => setStepIndex(2)}
                                    className="bg-transparent font-semibold hover:text-white py-2 px-4 border hover:border-transparent rounded"
                                    style={{borderColor: "#4FC1E0FF", color: "#4FC1E0FF", width: 100, height: 100}}>
                                <ThumbUpOffAltIcon/>
                                <div>
                                    Positive
                                </div>
                            </button>
                        </div>
                    </div>
                )
            case 2:
                return (
                    <div style={{textAlign: "center"}}>
                        <br/>
                        <h4>Thank you for your feedback</h4>
                        <br/>
                        <br/>
                        <div style={{
                            display: "flex",
                            flexDirection: "row",
                            alignItems: "center",
                            justifyContent: "center",
                            gap: 15
                        }}>
                            <button onClick={() => onFinish()}
                                className="bg-transparent font-semibold hover:text-white py-2 px-4 border hover:border-transparent rounded"
                                style={{borderColor: "#4FC1E0FF", color: "#4FC1E0FF", width: 250, height: 70}}>
                                <div>
                                    Go back
                                </div>
                            </button>
                        </div>
                    </div>
                )
        }
    }

    return (
        <div style={{
            position: "absolute",
            top: 0,
            bottom: 0,
            backgroundColor: "#FEFEFE",
            display: "flex",
            flexDirection: "column",
            alignItems: "center",
            justifyContent: "center"
        }}>
            <div>
                <img src={SplashImage} alt={"SplashScreen"}/>
                <div style={{display: "flex", flexDirection: "column", alignItems: "center", justifyContent: "center"}}>
                    {renderStep()}
                </div>
            </div>
        </div>
    );
}

export default StressedScreen;
