import React, { useRef, useState } from 'react';

import { TILE_WIDTH, TILE_HEIGHT } from '../lib/constants';

const TilePicker = ({
  imageData,
  onTileSelect
}) => {
  const canvasRef = useRef(null);
  const [ selectedX, setSelectedX ] = useState(0);
  const [ selectedY, setSelectedY ] = useState(0);

  const onClick = evt => {
    const { offsetX, offsetY } = evt.nativeEvent;
    const newSelectedX = Math.floor(offsetX / TILE_WIDTH);
    const newSelectedY = Math.floor(offsetY / TILE_HEIGHT);
    onTileSelect(newSelectedX, newSelectedY);
    setSelectedX(newSelectedX);
    setSelectedY(newSelectedY);
  };

  if (imageData) {
    const ctx = canvasRef.current.getContext('2d');
    window.requestAnimationFrame(() => {
      const realX = selectedX * TILE_WIDTH;
      const realY = selectedY * TILE_HEIGHT;
      ctx.drawImage(imageData, 0, 0);
      ctx.beginPath();
      ctx.lineWidth = 1;
      ctx.strokeStyle = 'red';
      ctx.rect(
        realX,
        realY,
        TILE_WIDTH,
        TILE_HEIGHT
      );
      ctx.stroke();
    });
  }

  return (
    <canvas
      ref={canvasRef}
      width={imageData && imageData.width}
      height={imageData && imageData.height}
      onClick={onClick}
    />
  );
};

export default TilePicker;
