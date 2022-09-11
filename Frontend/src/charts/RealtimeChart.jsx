import React, { useRef, useEffect } from 'react';

import {
  Chart, LineController, LineElement, Filler, PointElement, LinearScale, TimeScale, Tooltip,
} from 'chart.js';
import 'chartjs-adapter-moment';

Chart.register(LineController, LineElement, Filler, PointElement, LinearScale, TimeScale, Tooltip);

function RealtimeChart({
  data,
  width,
  height
}) {

  const canvas = useRef(null);
  const chartValue = useRef(null);
  const chartDeviation = useRef(null);

  useEffect(() => {
    const ctx = canvas.current;
    // eslint-disable-next-line no-unused-vars
    const chart = new Chart(ctx, {
      type: 'line',
      data: data,
      options: {
        layout: {
          padding: 20,
        },
        scales: {
          y: {
            grid: {
              drawBorder: false,
            },
            suggestedMin: 30,
            suggestedMax: 80,
            ticks: {
              maxTicksLimit: 5,
            },
          },
          x: {
            type: 'time',
            time: {
              parser: 'hh:mm:ss',
              unit: 'second',
              tooltipFormat: 'MMM DD, hh:mm:ss a',
              displayFormats: {
                second: 'hh:mm:ss',
              },
            },
            grid: {
              display: false,
              drawBorder: false,
            },
            ticks: {
              autoSkipPadding: 48,
              maxRotation: 0,
            },
          },
        },
        plugins: {
          legend: {
            display: false,
          },
          tooltip: {
            titleFont: {
              weight: '600',
            },
            callbacks: {
              label: (context) => context.parsed.y,
            },
          },
        },
        interaction: {
          intersect: false,
          mode: 'nearest',
        },
        animation: false,
        maintainAspectRatio: false,
        resizeDelay: 200,
      },
    });
    return () => chart.destroy();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [data]);

  return (
    <React.Fragment>
      <div className="px-5 py-3">
        <div className="flex items-start">
          <div ref={chartDeviation} className="text-sm font-semibold text-white px-1.5 rounded-full"></div>
        </div>
      </div>
      <div className="grow">
        <canvas ref={canvas} data={data} width={width} height={height}></canvas>
      </div>
    </React.Fragment>
  );
}

export default RealtimeChart;
