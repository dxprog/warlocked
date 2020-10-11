import { useState } from 'react';

const useImage = imageSrc => {
  const [ data, setData ] = useState(null);
  const [ loading, setLoading ] = useState(false);

  if (!loading && !data) {
    const imageEl = new Image();
    imageEl.addEventListener('load', () => {
      setData(imageEl);
      setLoading(false);
    });
    imageEl.src = imageSrc;
    setLoading(true);
  }

  return data;
};

export default useImage;
