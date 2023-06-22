import { Box, Stack, Typography } from "@mui/material";
import React, { useEffect, useState } from "react";
import { Broadcaster, Clip, Game, UIClip } from "../../globals";
import { Sidebar } from "./Sidebar";
import { useSearchParams } from "react-router-dom";
import { ScraperTab } from "./ScraperTab";
import { EditorTab } from "./EditorTab";
import { Api } from "../../Api";

export const StudioPage: React.FC = () => {
  const [cartItems, setCartItems] = useState<UIClip[]>([]);

  const removeCartItem = (item: UIClip) => {
    setCartItems(
      cartItems.filter(
        (_) => _.clip.id != item.clip.id || _.dupeIndex != item.dupeIndex
      )
    );
  };

  const addCartItem = (item: UIClip) => setCartItems([...cartItems, item]);

  const [presetGames, setPresetGames] = useState<Game[]>([]);
  const [presetBroadcasters, setPresetBroadcasters] = useState<Broadcaster[]>(
    []
  );

  useEffect(() => {
    const fetchGames = async () => {
      const res = await Api.get("/games");
      setPresetGames(res.data as Game[]);
    };
    const fetchBroadcasters = async () => {
      const res = await Api.get("/broadcasters");
      setPresetBroadcasters(res.data as Broadcaster[]);
    };

    try {
      fetchGames();
      fetchBroadcasters();
    } catch (err) {
      console.error(err);
    }
  }, []);

  const [params] = useSearchParams();
  const tab = params.get("tab") || "scraper";
  const [draggedClip, setDraggedClip] = useState<UIClip | null>(null);

  return (
    <Box height="100%" width="100%" display="flex">
      <Sidebar
        tab={tab}
        cartItems={cartItems}
        setCartItems={setCartItems}
        setDraggedClip={setDraggedClip}
      />
      <Box
        flex={1}
        sx={{
          backgroundColor: "rgb(60, 60, 60)",
          color: "white",
        }}
      >
        {tab == "scraper" ? (
          <ScraperTab
            addCartItem={addCartItem}
            presetGames={presetGames}
            presetBroadcasters={presetBroadcasters}
          />
        ) : (
          <EditorTab
            draggedClip={draggedClip}
            setDraggedClip={setDraggedClip}
            removeCartItem={removeCartItem}
          />
        )}
      </Box>
    </Box>
  );
};
