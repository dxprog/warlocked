import React, { useState } from 'react';

import useImage from '../lib/ImageLoader.jsx';
import TilePicker from './TilePicker.jsx';
import AnimatedTile from './AnimatedTile.jsx';

const TileCreator = () => {
  const tileImage = useImage('tileset.png');
  const [ selectedTile, setSelectedTile ] = useState({ x: 0, y: 0 });
  const [ frames, setFrames ] = useState([]);

  const addSelectedTileAsFrame = () => {
    setFrames([ ...frames, selectedTile ]);
  };

  const deleteFrame = frameIndexToRemove => {
    setFrames(frames.reduce((out, frame, index) => {
      if (index !== frameIndexToRemove) {
        out.push(frame);
      }
      return out;
    }, []));
  };

  return (
    <div className="tile-creator">
      <div className="tile-creator__image">
        <TilePicker
          imageData={tileImage}
          onTileSelect={(x, y) => setSelectedTile({ x, y })}
        />
      </div>
      <div className="tile-creator__form">
        <AnimatedTile imageData={tileImage} frames={frames} />
        <button
          type="button"
          onClick={addSelectedTileAsFrame}
        >
          Add Selected Tile As Frame
        </button>
        <ul className="tile-creator__frames">
          {frames.map((frame, index) => (
            <li
              className="tile-creator__frame"
              key={`tile-frame-${index}`}
            >
              <AnimatedTile imageData={tileImage} frames={[ frames[index] ]} />
              Frame {index + 1}
              <button
                type="button"
                onClick={() => deleteFrame(index)}
              >
                Delete
              </button>
            </li>
          ))}
        </ul>
      </div>
    </div>
  );
};

export default TileCreator;
