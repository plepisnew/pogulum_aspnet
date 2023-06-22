export const secondsToTwitchTimeStamp = (seconds: number): string => {
  seconds = Math.floor(seconds);
  if (seconds < 60) return `00:${pad(seconds)}`;
  let minutes = Math.floor(seconds / 60);
  seconds -= minutes * 60;
  if (minutes < 60) return `${pad(minutes)}:${pad(seconds)}`;
  let hours = Math.floor(minutes / 60);
  minutes -= hours * 60;
  return `${pad(hours)}:${pad(minutes)}:${pad(seconds)}`;
};

const pad = (paddable: number): string => {
  const paddableString = `${paddable}`;

  return paddableString.length == 1 ? "0" + paddableString : paddableString;
};
