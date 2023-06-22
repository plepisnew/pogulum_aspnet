import { Stack, Typography, Box } from "@mui/material";
import SearchIcon from "@mui/icons-material/Search";
import ConstructionIcon from "@mui/icons-material/Construction";
import { useCallback } from "react";
import { useSearchParams, useNavigate } from "react-router-dom";
import { ClipCard } from "./ClipCard";
import { Button, Divider } from "./StyledComponents";
import { Clip, UIClip } from "../../globals";
import { Fatbox } from "../../components/Fatbox";
import ControlPointDuplicateIcon from "@mui/icons-material/ControlPointDuplicate";

type Props = {
  tab: string;
  cartItems: UIClip[];
  setCartItems: React.Dispatch<React.SetStateAction<UIClip[]>>;
  setDraggedClip: React.Dispatch<React.SetStateAction<UIClip | null>>;
};

export const Sidebar: React.FC<Props> = ({
  tab,
  cartItems,
  setCartItems,
  setDraggedClip,
}) => {
  const navigate = useNavigate();

  const navigateToScraper = useCallback(() => navigate("?tab=scraper"), []);
  const navigateToEditor = useCallback(() => navigate("?tab=editor"), []);

  const removeClip = (index: number) =>
    setCartItems(cartItems.filter((_, clipIndex) => index != clipIndex));

  const duplicateClip = (index: number) => {
    const newItem = JSON.parse(JSON.stringify(cartItems[index])) as UIClip;

    newItem.open = false;
    newItem.dupeIndex = Date.now();

    setCartItems([
      ...cartItems.slice(0, index + 1),
      newItem,
      ...cartItems.slice(index + 1),
    ]);
  };

  const handleOpenCartItem = (index: number) => {
    setCartItems(
      cartItems.map((item, itemIndex) =>
        index == itemIndex
          ? {
              ...item,
              open: !item.open,
            }
          : item
      )
    );
  };

  return (
    <Stack
      direction="column"
      width="300px"
      p={4}
      spacing={3}
      sx={{
        background: "rgb(20, 20, 20)",
        borderRight: "2px solid rgb(10, 10, 10)",
        color: "white",
      }}
    >
      <Typography variant="h5" textAlign="center">
        Pogulum Studio
      </Typography>
      <Divider />
      <Button
        icon={<SearchIcon />}
        onClick={navigateToScraper}
        selected={tab == "scraper"}
      >
        Scrape Clips
      </Button>
      <Button
        icon={<ConstructionIcon />}
        onClick={navigateToEditor}
        selected={tab == "editor"}
      >
        Edit Clips
      </Button>
      <Divider />
      <Box
        flex={1}
        p={2}
        border="1px solid white"
        borderRadius="10px"
        overflow="scroll"
        sx={{
          background: "rgba(255, 255, 255, 0.1)",
        }}
        data-scroll="false"
      >
        {cartItems.length == 0 ? (
          <Fatbox center>
            <Typography fontSize="1.125rem" textAlign="center">
              Selected clips go here.
            </Typography>
          </Fatbox>
        ) : (
          <Stack spacing={2}>
            {cartItems.map((item, index) => (
              <ClipCard
                key={item.clip.id + item.dupeIndex}
                item={item}
                onRemove={() => removeClip(index)}
                onDuplicate={() => duplicateClip(index)}
                open={item.open}
                handleOpen={() => handleOpenCartItem(index)}
                setDraggedClip={setDraggedClip}
              />
            ))}
          </Stack>
        )}
      </Box>
    </Stack>
  );
};
