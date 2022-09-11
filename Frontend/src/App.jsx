import React, {useEffect, useState} from 'react';
import {
    Routes,
    Route,
    useLocation
} from 'react-router-dom';

import './css/style.css';

import './charts/ChartjsConfig';

import Dashboard from './pages/Dashboard';
import SplashScreen from "./pages/SplashScreen";
import StressedScreen from "./pages/StressedScreen";
import Mockups from "./pages/Mockups";
import OtherMockups from "./pages/OtherMockups";

function App() {

    const [splashScreen, setSplashScreen] = useState(true)
    const [stressedScreen, setStressedScreen] = useState(false)
    const [mockups, setMockups] = useState(false)
    const [alreadyWentTroughMockups, setAlreadyWentTroughMockups] = useState(false)
    const [alreadyShowedBanner, setAlreadyShowedBanner] = useState(false)

    const location = useLocation();

    function startTimer() {
        const interval = setInterval(() => {
            setSplashScreen(false)
            clearInterval(interval)
        }, 2000);
    }

    function goBackToHomeScreen(){
        setAlreadyShowedBanner(true)
        setStressedScreen(false)
    }

    useEffect(() => {
        document.querySelector('html').style.scrollBehavior = 'auto'
        window.scroll({top: 0})
        document.querySelector('html').style.scrollBehavior = ''
    }, [location.pathname]); // triggered on route change

    function finishMockups(){

        if (alreadyWentTroughMockups !== true)
            setAlreadyWentTroughMockups(true)

        setMockups(false)
    }

    function renderPage() {

        if (splashScreen) {
            return (
                <SplashScreen style={{backgroundColor: "#FEFEFEFF"}} onClick={() => startTimer()}/>
            )
        }
        else if (stressedScreen) {
            return (
                <StressedScreen onFinish={goBackToHomeScreen}/>
            )
        }
        else if (mockups) {
            if (alreadyWentTroughMockups){
                return (<OtherMockups onFinish={() => finishMockups()}/>)
            }
            return (
                <Mockups onFinish={() => finishMockups()}/>
            )
        }
        return (
            <Dashboard setStressedScreen={() => {setStressedScreen(true)}} setMockups={() => setMockups(true)} alreadyShowedBanner={alreadyShowedBanner}/>
        )
    }

    return (
        <>
            <Routes>
                <Route exact path="/" element={renderPage()}/>
            </Routes>
        </>
    );
}

export default App;
