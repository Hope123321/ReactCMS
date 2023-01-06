import * as React from "react";
import {
  Avatar,
  Divider,
  IconButton,
  Link,
  ListItemIcon,
  Menu,
  MenuItem,
  Stack,
  Typography,
  useTheme,
} from "@mui/material";
import {
  AccountCircle,
  Logout,
  PersonAdd,
  Settings,
} from "@mui/icons-material";
import { useContext, useState } from "react";
import UserContext from "../../contexts/UserContext";
import AlertDialog from "../AlertDialog/AlertDialog";
import { Link as RouterLink } from "react-router-dom";

export default function UserAvatar() {
  const userContext: any = useContext(UserContext);
  const user = userContext[0];
  //控制人物登入事件
  const [anchorEl, setAnchorEl] = React.useState<null | HTMLElement>(null);
  const handleMenu = (event: React.MouseEvent<HTMLElement>) => {
    setAnchorEl(event.currentTarget);
  };
  const handleClose = () => {
    setAnchorEl(null);
  };

  // #region 開啟登出確認視窗
  const [LogOutOpen, SetLogOutOpen] = useState<boolean>(false);
  //登出
  function MenuClickByLogOut() {
    SetLogOutOpen(true);
  }
  // #endregion

  //let
  return (
    <div>
      <IconButton
        size="large"
        aria-label="account of current user"
        aria-controls="menu-appbar"
        aria-haspopup="true"
        onClick={handleMenu}
        color="inherit"
      >
        <Stack
          direction="row"
          spacing={1}
          justifyContent="center"
          alignItems="center"
        >
          <AccountCircle fontSize="large" />
          <Typography variant="h6">{user.UserNa}</Typography>
        </Stack>
      </IconButton>
      {/* <Menu
          id="menu-appbar"
          anchorEl={anchorEl}
          anchorOrigin={{
            vertical: 'top',
            horizontal: 'right',
          }}
          keepMounted
          transformOrigin={{
            vertical: 'top',
            horizontal: 'right',
          }}
          open={Boolean(anchorEl)}
          onClose={handleClose}
        >
          <MenuItem onClick={handleClose}>Profile</MenuItem>
          <MenuItem onClick={handleClose}>My account</MenuItem>
        </Menu> */}
      <Menu
        anchorEl={anchorEl}
        id="account-menu"
        open={Boolean(anchorEl)}
        onClose={handleClose}
        onClick={handleClose}
        PaperProps={{
          elevation: 0,
          sx: {
            overflow: "visible",
            filter: "drop-shadow(0px 2px 8px rgba(0,0,0,0.32))",
            mt: 1.5,
            "& .MuiAvatar-root": {
              width: 32,
              height: 32,
              ml: -0.5,
              mr: 1,
            },
            "&:before": {
              content: '""',
              display: "block",
              position: "absolute",
              top: 0,
              right: 14,
              width: 10,
              height: 10,
              bgcolor: "background.paper",
              transform: "translateY(-50%) rotate(45deg)",
              zIndex: 0,
            },
          },
        }}
        transformOrigin={{ horizontal: "right", vertical: "top" }}
        anchorOrigin={{ horizontal: "right", vertical: "bottom" }}
      >
        <Link component={RouterLink} to="/Profile">
          <MenuItem color="inherit">
            <Avatar /> 會員資料
          </MenuItem>
        </Link>
        <MenuItem></MenuItem>
        <Divider />
        <Link component={RouterLink} to="/AddAccount">
        <MenuItem>
          <ListItemIcon>
            <PersonAdd fontSize="small" />
          </ListItemIcon>
          新增會員
        </MenuItem>
        </Link>
        <Link component={RouterLink} to="/Settings">
        <MenuItem>
            <ListItemIcon>
              <Settings fontSize="small" />
            </ListItemIcon>
            系統設定
        </MenuItem>
        </Link>
        <MenuItem onClick={MenuClickByLogOut}>
          <ListItemIcon>
            <Logout fontSize="small" />
          </ListItemIcon>
          會員登出
        </MenuItem>
      </Menu>
      {/* 登出視窗 */}
      <AlertDialog
        message="您是否要登出?"
        caption="登出會清除暫存的工作階段，如要繼續請按確認。"
        Open={LogOutOpen}
        SetOpen={SetLogOutOpen}
        ClickConfirm={() => {
          //清除登入資料
          userContext[1](null);
        }}
      />
    </div>
  );
}
