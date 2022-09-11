import React from 'react';
import RealtimeChart from '../../charts/RealtimeChart';

import {tailwindConfig, hexToRGB} from '../../utils/Utils';

function HRVDataCard({selectedSession}) {

    function formatLabels() {
        return selectedSession.events.map((event) => new Date(event.timestamp.seconds * 1000))
    }


    function formatData() {
        const data = []
        let value = 79
        selectedSession.events.forEach((event, index) => {

            if (index < 83) {
                if (Math.random() >= 0.5) {
                    data.push(value - 0.2)
                }

                if (Math.random() < 0.5) {
                    data.push(value + 0.1)
                }
            }
            if (index > 83) {
                if (Math.random() >= 0.5)
                    value -= 0.15

                data.push(value)
            }


        })

        return data
    }

    const labels = selectedSession === undefined ? [0] : formatLabels()
    const data = selectedSession === undefined ? [0] : formatData()

    let baseText = `Heart Rate Variability`

    const chartData = {
        labels: labels,
        datasets: [
            {
                data: data,
                fill: true,
                backgroundColor: `rgba(220, 243, 249, 0.5)`,
                borderColor: `rgb(79, 193, 224)`,
                borderWidth: 2,
                tension: 0,
                pointRadius: 0,
                pointHoverRadius: 3,
                pointBackgroundColor: tailwindConfig().theme.colors.indigo[500],
                clip: 20,
            },
        ],
    };

    return (
        <div
            className="flex flex-col col-span-full sm:col-span-6 bg-white shadow-lg rounded-sm border border-slate-200">
            <header className="px-5 py-4 border-b border-slate-100 flex items-center">
                <h3 className="font-semibold text-slate-800">{baseText}</h3>
            </header>
            <RealtimeChart data={chartData} width={595} height={248}/>
        </div>
    );
}

export default HRVDataCard;
