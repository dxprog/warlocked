import React, { useEffect, useRef } from 'react';

import { TILE_WIDTH, TILE_HEIGHT, FRAME_DELAY } from '../lib/constants';

const AnimatedTile = ({ imageData, frames }) => {
  const canvasRef = useRef(null);
  const startTime = Date.now();

  useEffect(() => {
    let frameHandle;
    const render = () => {
      const ctx = canvasRef.current.getContext('2d');
      const currentTime = Date.now();
      const currentFrameIndex = (
        Math.floor((currentTime - startTime) / FRAME_DELAY) % frames.length
      );
      const currentFrame = frames[currentFrameIndex];

      if (frames.length) {
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
        frameHandle = requestAnimationFrame(render);
      }
    };
    render();

    return () => {
      cancelAnimationFrame(frameHandle);
    }
  });

  return (
    <canvas ref={canvasRef} width="24" height="24" />
  );
};

export default AnimatedTile;
