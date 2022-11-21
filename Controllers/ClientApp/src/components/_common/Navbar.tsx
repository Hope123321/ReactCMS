import { useState, useEffect } from "react";

import AppBar from "@mui/material/AppBar";
import Drawer from "@mui/material/Drawer";
import Toolbar from "@mui/material/Toolbar";

import List from "@mui/material/List";
import ListItem from "@mui/material/ListItem";
import ListItemText from "@mui/material/ListItemText";
import ListItemButton from "@mui/material/ListItemButton";
import ListItemIcon from "@mui/material/ListItemIcon";
import Collapse from "@mui/material/Collapse";

import Divider from "@mui/material/Divider";
import InboxIcon from "@mui/icons-material/MoveToInbox";
import MailIcon from "@mui/icons-material/Mail";
import IconButton from "@mui/material/IconButton";
import MenuIcon from "@mui/icons-material/Menu";
import ExpandLess from "@mui/icons-material/ExpandLess";
import ExpandMore from "@mui/icons-material/ExpandMore";

import Box from "@mui/material/Box";

import CssBaseline from "@mui/material/CssBaseline";
import Typography from "@mui/material/Typography";
import { useTheme } from "@mui/material/styles";
import useMediaQuery from "@mui/material/useMediaQuery";

import { Link } from "react-router-dom";
import { Menu } from "../types/_common/Menu/Menu";
import React from "react";

const styles = {
  navlinks: {
    marginLeft: 5,
    display: "flex",
  },
  logo: {
    flexGrow: 1,
    cursor: "pointer",
    textDecoration: "none",
    color: "#FFFFFF",
  },
  link: {
    textDecoration: "none",
    color: "#000000",
    width: "100%",
  },
};

function Navbar() {
  //console.log(process.env);
  const menuList: Array<Menu> = [
    { MenuNo: "Home", MenuNa: "個人首頁", MenuLink: "/", parentMenuNo: "" },
    {
      MenuNo: "Contact",
      MenuNa: "聯絡我們",
      MenuLink: "/Contact",
      parentMenuNo: "",
    },
    { MenuNo: "Faq", MenuNa: "常見問題", MenuLink: "/Faq", parentMenuNo: "" },
    {
      MenuNo: "About",
      MenuNa: "關於我們",
      MenuLink: "/About",
      parentMenuNo: "",
    },
    {
      MenuNo: "POS",
      MenuNa: "前台功能管理",
      MenuLink: "",
      parentMenuNo: "",
    },
    {
      MenuNo: "P0001",
      MenuNa: "系統參數設定",
      MenuLink: "/POS/P0001",
      parentMenuNo: "POS",
    },
    {
      MenuNo: "Manager",
      MenuNa: "後台功能管理",
      MenuLink: "",
      parentMenuNo: "",
    },
    {
      MenuNo: "M0001",
      MenuNa: "系統參數設定",
      MenuLink: "/Manager/M0001",
      parentMenuNo: "Manager",
    },
    {
      MenuNo: "M0002",
      MenuNa: "客製參數設定",
      MenuLink: "/Manager/M0002",
      parentMenuNo: "Manager",
    },
    {
      MenuNo: "M0003",
      MenuNa: "收銀功能設定",
      MenuLink: "/Manager/M0003",
      parentMenuNo: "Manager",
    },
    {
      MenuNo: "M0004",
      MenuNa: "外部系統設定",
      MenuLink: "/Manager/M0004",
      parentMenuNo: "Manager",
    },
  ];

  const drawerWidth = 240;
  const theme = useTheme();
  const isMobile = useMediaQuery(theme.breakpoints.down("md"));
  //sidebar開關控制，
  const [openSidebar, setOpenSidebar] = useState(false);

  const [openMenuNested, setOpenMenuNested] = useState<Array<string>>([]);

  //State監測
  useEffect(() => {
    console.log("openMenuNested=");
    console.log(openMenuNested);
  });

  return (
    <>
      <CssBaseline />
      <AppBar
        position="fixed"
        sx={{ zIndex: (theme) => theme.zIndex.drawer + 1 }}
      >
        <Toolbar>
          {isMobile ? (
            <IconButton
              onClick={() => setOpenSidebar(!openSidebar)}
              style={{ color: "white" }}
            >
              <MenuIcon />
            </IconButton>
          ) : (
            ""
                  )
                  }
          <Link to="/" style={styles.logo}>
            <Typography variant="h5" noWrap component="div">
              CMS
            </Typography>
          </Link>
        </Toolbar>
      </AppBar>
      <Drawer
        //移動裝置時Sidebar改為懸浮顯示，非移動裝置時固定於左側
        variant={isMobile ? "temporary" : "permanent"}
        sx={{
          width: drawerWidth,
          flexShrink: 0,
          [`& .MuiDrawer-paper`]: {
            width: drawerWidth,
            boxSizing: "border-box",
          },
        }}
        open={openSidebar}
      >
        <Toolbar />
        <Box sx={{ overflow: "auto" }}>
          {GetMenu(menuList)}
          {/* <List>
            {["Contact", "Faq"].map((text, index) => (
              <ListItem key={text} disablePadding>
                <Link to={text} style={styles.link}>
                  <ListItemButton
                    onClick={() => {
                      setOpenSidebar(false);
                    }}
                  >
                    <ListItemIcon>
                      {index % 2 === 0 ? <InboxIcon /> : <MailIcon />}
                    </ListItemIcon>
                    <ListItemText primary={text} />
                  </ListItemButton>
                </Link>
              </ListItem>
            ))}
          </List>
          <Divider />
          <List>
            {["About"].map((text, index) => (
              <ListItem key={text} disablePadding>
                <Link to={text} style={styles.link}>
                  <ListItemButton
                    onClick={() => {
                      setOpenSidebar(false);
                    }}
                  >
                    <ListItemIcon>
                      {index % 2 === 0 ? <InboxIcon /> : <MailIcon />}
                    </ListItemIcon>
                    <ListItemText primary={text} />
                  </ListItemButton>
                </Link>
              </ListItem>
            ))}
          </List> */}
        </Box>
      </Drawer>
    </>
  );

  // #region private function

  //NestedMenu縮合控制
  function NestedMenuClick(menuNo: string) {
    let tmp = openMenuNested.concat();
    let index = openMenuNested.findIndex((x) => {
      return x === menuNo;
    });
    if (index !== -1) {
      tmp.splice(index);
    } else {
      tmp.push(menuNo);
    }
    setOpenMenuNested(tmp);
  }
  //確認NestedMenu Collapse狀態
  function CheckMenuCollapse(menuNo: string) {
    let ret = openMenuNested.some((x) => {
      return x === menuNo;
    });
    return ret;
  }
  //取得Menu
  function GetMenu(menuList: Array<Menu>): JSX.Element {
    return (
      <>
      <List>
        {
          //Nested Menu Area(兩層)
          menuList
            .filter((item) => {
              return item.MenuLink === "" && item.parentMenuNo === "";
            })
            .map((item, index) => (
              <>
                <ListItemButton
                  onClick={() => {
                    NestedMenuClick(item.MenuNo);
                  }}
                >
                  <ListItemIcon>
                    <InboxIcon />
                  </ListItemIcon>
                  <ListItemText primary={item.MenuNa} />
                  {openMenuNested.some((x) => {
                    return x === item.MenuNo;
                  }) ? (
                    <ExpandLess />
                  ) : (
                    <ExpandMore />
                  )}
                </ListItemButton>
                <Collapse
                  in={CheckMenuCollapse(item.MenuNo)}
                  timeout="auto"
                  unmountOnExit
                >
                  <List component="div" disablePadding>
                    {menuList
                      .filter((x) => {
                        return x.parentMenuNo === item.MenuNo;
                      })
                      .map((subItem) => (
                        <ListItem key={subItem.MenuNo} disablePadding>
                          <Link to={subItem.MenuLink} style={styles.link}>
                            <ListItemButton
                              sx={{ pl: 4 }}
                              onClick={() => {
                                setOpenSidebar(false);
                              }}
                            >
                              <ListItemIcon>
                                {index % 2 === 0 ? <InboxIcon /> : <MailIcon />}
                              </ListItemIcon>
                              <ListItemText primary={subItem.MenuNa} />
                            </ListItemButton>
                          </Link>
                        </ListItem>
                      ))}
                  </List>
                </Collapse>
              </>
            ))
        }
        </List>
        <Divider />
        <List>
        {
          //Basic Menu Area
          menuList
            .filter((item) => {
              return item.MenuLink !== "" && item.parentMenuNo === "";
            })
            .map((item, index) => (
              <>
                {/* <ListItem key={item.MenuNo} disablePadding> */}
                  <Link to={item.MenuLink} style={styles.link}>
                    <ListItemButton
                      onClick={() => {
                        setOpenSidebar(false);
                      }}
                    >
                      <ListItemIcon>
                        {index % 2 === 0 ? <InboxIcon /> : <MailIcon />}
                      </ListItemIcon>
                      <ListItemText primary={item.MenuNa} />
                    </ListItemButton>
                  </Link>
                {/* </ListItem> */}
              </>
            ))
        }
        </List>
      </>
    );
  }

  // #endregion
}
export default Navbar;
