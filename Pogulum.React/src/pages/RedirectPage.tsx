import React, { useEffect } from "react";
import { useNavigate } from "react-router-dom";

export const RedirectPage: React.FC = () => {
  const navigate = useNavigate();

  useEffect(() => {
    navigate("/studio");
  }, []);

  return <></>;
};
