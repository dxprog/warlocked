import React, { useEffect, useRef, useState } from 'react';

import { TILE_WIDTH, TILE_HEIGHT } from '../lib/constants';

const AnimatedTile = ({ imageData, frames }) => {
  const canvasRef = useRef(null);
  const [ frame, setFrame ] = useState(0);

  useEffect(() => {
    const ctx = canvasRef.current.getContext('2d');
    const frameHandle = requestAnimationFrame(() => {
      let currentFrameIndex = frame;
      const currentFrame = frames[currentFrameIndex];

      ctx.drawImage(
        imageData,
        currentFrame.x * TILE_WIDTH,
        currentFrame.y * TILE_HEIGHT,
        TILE_WIDTH,
        TILE_HEIGHT,
        0,
        0,
        TILE_WIDTH,
        TILE_HEIGHT,
      );
      currentFrameIndex = currentFrameIndex + 1 < frames.length ? currentFrameIndex + 1 : 0;
      setTimeout(() => setFrame(currentFrameIndex), 250);
    });

    return () => {
      cancelAnimationFrame(frameHandle);
    }
  });

  return (
    <canvas ref={canvasRef} width="24" height="24" />
  );
};

export default AnimatedTile;
