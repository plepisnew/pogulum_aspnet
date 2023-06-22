export const parseJwt = (token: string): any => {
  const base64Url = token.split(".")[1];
  const base64 = base64Url.replace(/-/g, "+").replace(/_/g, "/");
  var jsonPayload = decodeURIComponent(
    window
      .atob(base64)
      .split("")
      .map(function (c) {
        return "%" + ("00" + c.charCodeAt(0).toString(16)).slice(-2);
      })
      .join("")
  );

  return JSON.parse(jsonPayload);
};

export const getId = (user: any): string => {
  const id: string =
    user[
      "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"
    ];

  return id ?? new Error("Attempted to access an invalid JWT");
};

export const getName = (user: any): string => {
  const name: string =
    user["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"];

  return name ?? new Error("Attempted to access an invalid JWT");
};
