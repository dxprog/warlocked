import React, { useState } from 'react';

import TileCreator from '../components/TileCreator.jsx';
import TileContext from '../lib/TileContext.jsx';

const App = () => {
  const [ tiles, setTiles ] = useState([]);

  const addTile = (tile, index) => {
    const newTiles = [ ...tiles ];
    if (index) {
      newTiles[index] = tile;
    } else {
      newTiles.push(tile);
    }
    setTiles(newTiles);
  };

  return (
    <TileContext.Provider value={{ tiles, addTile }}>
      <TileCreator />
      <div>{tiles.length}</div>
    </TileContext.Provider>
  );
};

export default App;
