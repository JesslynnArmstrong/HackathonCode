import React from 'react';

import Steps from '../../images/summary2.png';
import Summary from '../../images/summary.png';


function CollectableCard({title, isSteps}) {

    const height = isSteps ? 250 : 250

    return (
        <div
            className="flex flex-col col-span-full sm:col-span-6 bg-white shadow-lg rounded-sm border border-slate-200">
            <header className="px-5 py-4 border-b border-slate-100 flex items-center">
                <h3 className="font-semibold text-slate-800">{title}</h3>
            </header>
            <React.Fragment>
                <div className="px-5 py-3">
                    <div className="flex items-start">
                        <div className="text-sm font-semibold text-white px-1.5 rounded-full"></div>
                    </div>
                </div>
                {
                    isSteps === false &&
                    <div className="grow">
                        <br/>
                        <br/>
                        <br/>
                        <br/>
                        <br/>
                        <div style={{
                            width: 595,
                            height: height,
                            display: "flex",
                            alignItems: "center",
                            justifyContent: "center",
                            marginLeft: -53
                        }}>
                            <img width={450} src={Summary} alt={"summary"}/>
                        </div>
                        <br/>
                        <br/>
                        <br/>
                        <br/>
                        <br/>
                    </div>
                }
                {
                    isSteps === true &&
                    <div className="grow">
                        <div style={{
                            width: 595,
                            height: height,
                            display: "flex",
                            alignItems: "center",
                            justifyContent: "center",
                            marginLeft: -53
                        }}>
                            <img width={400} src={Steps} alt={"summary"}/>
                        </div>
                        <br/>
                        <br/>
                    </div>
                }
            </React.Fragment>
        </div>
    );
}

export default CollectableCard;
