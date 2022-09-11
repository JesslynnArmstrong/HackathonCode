import React, { useState, useRef, useEffect } from 'react';
import Transition from '../../utils/Transition';

function FilterButton(props) {

  const [selectedSessionName, setSelectedSessionName] = useState(undefined)
  const [dropdownOpen, setDropdownOpen] = useState(false);

  const trigger = useRef(null);
  const dropdown = useRef(null);

  // close on click outside
  useEffect(() => {
    const clickHandler = ({ target }) => {
      if (!dropdownOpen || dropdown.current.contains(target) || trigger.current.contains(target)) return;
      setDropdownOpen(false);
    };
    document.addEventListener('click', clickHandler);
    return () => document.removeEventListener('click', clickHandler);
  });

  // close if the esc key is pressed
  useEffect(() => {
    const keyHandler = ({ keyCode }) => {
      if (!dropdownOpen || keyCode !== 27) return;
      setDropdownOpen(false);
    };
    document.addEventListener('keydown', keyHandler);
    return () => document.removeEventListener('keydown', keyHandler);
  });

  function onClickSelection(session){
    props.updateSelectedSession(session.documentId)
    setDropdownOpen(!dropdownOpen)
    setSelectedSessionName(session.sessionName)
  }

  const text = selectedSessionName === undefined ? "Select Session" : selectedSessionName

  return (
    <div className="relative inline-flex">
      <button
        ref={trigger}
        className="btn bg-white border-slate-200 hover:border-slate-300 text-slate-500 hover:text-slate-600"
        aria-haspopup="true"
        onClick={() => setDropdownOpen(!dropdownOpen)}
        aria-expanded={dropdownOpen}
      >
        {text}
      </button>
      <Transition
        show={dropdownOpen}
        tag="div"
        className="origin-top-right z-10 absolute top-full left-0 right-auto md:left-auto md:right-0 min-w-56 bg-white border border-slate-200 pt-1.5 rounded shadow-lg overflow-hidden mt-1"
        enter="transition ease-out duration-200 transform"
        enterStart="opacity-0 -translate-y-2"
        enterEnd="opacity-100 translate-y-0"
        leave="transition ease-out duration-200"
        leaveStart="opacity-100"
        leaveEnd="opacity-0"
      >
        <div ref={dropdown}>
          <div className="text-xs font-semibold text-slate-400 uppercase pt-1.5 pb-2 px-4" style={{width: 300}}>Sessions</div>
          <ul className="mb-4">
            {
              props.sessions.map((session, index) => (
                  <li className="py-1 px-3" key={index}>
                    <label className="flex items-center" onClick={() => onClickSelection(session)}>
                      <span className="text-sm font-medium ml-2">{session.sessionName}</span>
                    </label>
                  </li>
              ))
            }
          </ul>
        </div>
      </Transition>
    </div>
  );
}

export default FilterButton;
