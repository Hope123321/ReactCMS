import { Backdrop, CircularProgress } from "@mui/material";
import * as React from "react";

export default function Loading(props: LoadingProps) {
  function handleClick() {
    if (props.IsClickHide) {
      props.setOpen(false);
    }
  }
  return (
    <div role="presentation" onClick={handleClick}>
      <Backdrop
        sx={{ color: "#fff", zIndex: (theme) => theme.zIndex.drawer + 1 }}
        open={props.Open}
        onClick={handleClick}
      >
        <CircularProgress color="inherit" />
      </Backdrop>
    </div>
  );
}

interface LoadingProps {
  Open: boolean;
  setOpen: Function;
  IsClickHide?: boolean;
}
