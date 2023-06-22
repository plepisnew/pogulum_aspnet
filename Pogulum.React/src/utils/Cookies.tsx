import React, { useContext, createContext } from "react";

type CookieMap = {
  [key: string]: string;
};

type CookieContext = {
  cookies: CookieMap;
};

export const CookieContext = createContext<CookieContext>({
  cookies: {},
});

export const CookieProvider: React.FC<{ children: React.ReactNode }> = ({
  children,
}) => {
  const cookies = document.cookie.split("; ");

  const cookieMap: CookieMap = {};

  for (const cookie of cookies) {
    const [key, value] = cookie.split("=");
    cookieMap[key] = value;
  }

  return (
    <CookieContext.Provider value={{ cookies: cookieMap }}>
      {children}
    </CookieContext.Provider>
  );
};

export const useCookies = () => useContext(CookieContext);
