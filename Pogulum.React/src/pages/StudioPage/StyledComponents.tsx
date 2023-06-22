import {
  Button as MuiButton,
  Divider as MuiDivider,
  TextField as MuiTextField,
  IconButton as MuiIconButton,
  Autocomplete as MuiAutocomplete,
  Tooltip as MuiTooltip,
  ButtonProps,
  IconButtonProps,
  styled,
  InputAdornment,
  TooltipProps,
  tooltipClasses,
} from "@mui/material";
import CheckIcon from "@mui/icons-material/Check";

export const Button: React.FC<
  {
    lightness?: number;
    children: React.ReactNode;
    icon?: React.ReactElement;
    selected?: boolean;
  } & ButtonProps
> = ({ lightness = 255, children, icon, selected, ...buttonProps }) => (
  <MuiButton
    {...buttonProps}
    sx={{
      borderColor: `rgb(${lightness}, ${lightness}, ${lightness})`,
      color: `rgb(${lightness}, ${lightness}, ${lightness})`,
      "&:hover": {
        borderColor:
          lightness < 215
            ? `rgb(${lightness + 40}, ${lightness + 40}, ${lightness + 40})`
            : "white",
        borderRightWidth: selected ? "5px" : "1px",
      },
      borderRightWidth: selected ? "5px" : "1px",
      ...buttonProps.sx,
    }}
    variant="outlined"
  >
    {icon ? (
      <>
        {children}&nbsp;
        {icon}
      </>
    ) : (
      <>{children}</>
    )}
  </MuiButton>
);

export const Divider: React.FC<{ fullWidth?: boolean; color?: string }> = ({
  fullWidth = true,
  color = "white",
}) => (
  <MuiDivider
    variant={fullWidth ? "fullWidth" : "middle"}
    sx={{
      backgroundColor: color,
    }}
  />
);

export const IconButton: React.FC<
  {
    disabled: boolean;
    children: React.ReactNode;
    left?: boolean;
    right?: boolean;
  } & IconButtonProps
> = ({ disabled, children, left, right, ...iconButtonProps }) => {
  return (
    <MuiIconButton
      {...iconButtonProps}
      disableRipple={disabled}
      sx={{
        backgroundColor: "rgba(0, 0, 0, 0.4)",
        "&:hover": {
          backgroundColor: "rgba(0, 0, 0, 0.3)",
        },
        color: disabled ? "rgb(100, 100, 100)" : "white",
        borderRadius: left ? "10px 0 0 10px" : right ? "0 10px 10px 0" : "",
        ...iconButtonProps.sx,
      }}
    >
      {children}
    </MuiIconButton>
  );
};

export const TextField = styled(MuiTextField)({
  input: {
    color: "white",
    borderColor: "white",
    paddingTop: "10px",
    paddingBottom: "10px",
  },
  ".MuiOutlinedInput-notchedOutline": {
    borderColor: "rgb(200, 200, 200)",
  },
  "&:hover .MuiOutlinedInput-notchedOutline": {
    borderColor: "white",
  },
});

export const Autocomplete: React.FC<{
  options: { name: string }[];
  input: string;
  setInput: React.Dispatch<React.SetStateAction<string>>;
  placeholder?: string;
  success?: boolean;
}> = ({ options, input, setInput, success, placeholder }) => {
  const successColor = "#76FF03";

  return (
    <MuiAutocomplete
      freeSolo
      fullWidth
      options={options.map((option) => option.name)}
      getOptionLabel={(option) => option as string}
      inputValue={input}
      onInputChange={(e, newInputValue) => {
        setInput(newInputValue);
      }}
      disableClearable
      renderInput={(params) => (
        <TextField
          {...params}
          placeholder={placeholder}
          InputProps={{
            endAdornment: (
              <InputAdornment position="end">
                {success && <CheckIcon sx={{ color: successColor }} />}
              </InputAdornment>
            ),
          }}
        />
      )}
      sx={{
        ".MuiInputBase-root": {
          paddingTop: 0,
          paddingBottom: 0,
        },
        "&:hover .MuiOutlinedInput-notchedOutline": {
          borderColor: success ? successColor : "white",
        },
        "& .MuiOutlinedInput-notchedOutline": {
          borderColor: success ? successColor : "white",
        },
      }}
    />
  );
};

export const Tooltip = styled(({ className, ...props }: TooltipProps) => (
  <MuiTooltip {...props} arrow classes={{ popper: className }} />
))({
  [`& .${tooltipClasses.tooltip}`]: {
    backgroundColor: "black",
    fontFamily: "Montserrat",
  },
});
