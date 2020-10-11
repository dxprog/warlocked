import React from 'react';

const TileContext = React.createContext(
  {
    tiles: [],
    addTile: () => {}
  }
);

export default TileContext;
