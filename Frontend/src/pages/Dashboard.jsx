import React, {useEffect, useState} from 'react';

import Sidebar from '../partials/Sidebar';
import Header from '../partials/Header';
import FilterButton from '../partials/actions/FilterButton';
import SessionDataCard from '../partials/dashboard/SessionDataCard';

import {firestore} from "../services/FirebaseService";

import { collection, query, where, getDocs } from "firebase/firestore"
import HRVDataCard from "../partials/dashboard/HRVDataCard";
import Banner from "../partials/dashboard/Banner";
import CollectableCard from "../partials/dashboard/CollectableCard";
import StressedBanner from "../partials/dashboard/StressedBanner";

function Dashboard({setStressedScreen, setMockups, alreadyShowedBanner}) {

  const [sidebarOpen, setSidebarOpen] = useState(false);
  const [sessions, setSessions] = useState([])

  const [selectedSession, setSelectedSession] = useState(undefined)
  const [showBanner, setShowBanner] = useState(false)
  const [showHistory, setShowHistory] = useState(true)

  useEffect(() => {
    getSessions().then(sessions => setSessions(sessions))
  },[])

  async function getSessions() {
    const snapshot = await getDocs(collection(firestore, "sessions"));
    return snapshot.docs.map((doc) => doc.data())
  }

  function updateSelectedSession(sessionDocumentId){
    const matchingSession = sessions.filter(session => session.documentId === sessionDocumentId)[0]
    setSelectedSession(matchingSession)
  }

  const buttonText = showHistory ? "Show History" : "Show Recent"

  useEffect(() => {
    if (alreadyShowedBanner)
      return

    const interval = setInterval(() => {
      setShowBanner(true)
      clearInterval(interval)
    }, 10000);
  }, []);

  return (
    <div className="flex h-screen overflow-hidden">
      <Sidebar sidebarOpen={sidebarOpen} setSidebarOpen={setSidebarOpen} />
      <div className="relative flex flex-col flex-1 overflow-y-auto overflow-x-hidden">
        <Header sidebarOpen={sidebarOpen} setSidebarOpen={setSidebarOpen} />
        <main>
          <div className="px-4 sm:px-6 lg:px-8 py-8 w-full max-w-9xl mx-auto">
            {
              showBanner === true &&
                <StressedBanner setStressedScreen={setStressedScreen}/>
            }
            <div className="sm:flex sm:justify-between sm:items-center mb-8">
              <div className="grid grid-flow-col sm:auto-cols-max justify-between w-full gap-12">
                <FilterButton sessions={sessions} updateSelectedSession={updateSelectedSession} />
                <div className="relative inline-flex">
                  <button
                      className="btn bg-white border-slate-200 hover:border-slate-300 text-slate-500 hover:text-slate-600"
                      aria-haspopup="true"
                      onClick={() => setShowHistory(!showHistory)}
                  >
                    {buttonText}
                  </button>
                </div>
              </div>
            </div>
            <div className="grid grid-cols-12 gap-6">
              {
                showHistory === true &&
                  <>
                    <SessionDataCard selectedSession={selectedSession}/>
                    <HRVDataCard selectedSession={selectedSession}/>
                  </>
              }
              {
                  showHistory === false &&
                  <>
                    <CollectableCard title={"Last month's stress summary"} isSteps={false}/>
                    <CollectableCard title={"Last month's summary"} isSteps={true}/>
                  </>
              }
              <br/>
              <br/>
              <br/>
              <br/>
              <br/>
            </div>
          </div>
        </main>
        <Banner setMockups={setMockups}/>
      </div>
    </div>
  );
}

export default Dashboard;
