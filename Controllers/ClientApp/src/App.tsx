import React from "react";
import { BrowserRouter as Router, Route } from "react-router-dom";
import { Routes } from "react-router-dom";
import Navbar from "./components/_common/Navbar";
import Toolbar from "@mui/material/Toolbar";
import Box from "@mui/material/Box";
import Home from "./pages/_common/home";
import About from "./pages/_common/about";
import Contact from "./pages/_common/contact";
import Faq from "./pages/_common/faq";
import P0001 from "./pages/POS/P0001";
import M0002 from "./pages/Manager/M0002";
import M0003 from "./pages/Manager/M0003";
import M0004 from "./pages/Manager/M0004";
import M0005 from "./pages/Manager/M0005";
import M0001 from "./pages/Manager/M0001";


function App() {
  return (
    <Router>
      <Box sx={{ display: "flex" }}>
        <Navbar />
        <Box component="main" sx={{ flexGrow: 1, p: 3 }}>
          <Toolbar />
          <Routes>
            {/* 基本功能 */}
            <Route path="/" element={<Home />} />
            <Route path="/about" element={<About />} />
            <Route path="/contact" element={<Contact />} />
            <Route path="/faq" element={<Faq />} />
            {/* 前台管理模組 */}
            <Route path="/POS/P0001" element={<P0001 />} />
            {/* 後台管理模組 */}
            <Route path="/Manager/M0001" element={<M0001 />} />
            <Route path="/Manager/M0002" element={<M0002 />} />
            <Route path="/Manager/M0003" element={<M0003 />} />
            <Route path="/Manager/M0004" element={<M0004 />} />
            <Route path="/Manager/M0005" element={<M0005 />} />
          </Routes>
        </Box>
      </Box>
      {/* <Switch>
        <Route exact path="/" component={Home} />
        <Route path="/about" component={About} />
        <Route path="/contact" component={Contact} />
        <Route path="/faq" component={Faq} />
      </Switch> */}
    </Router>
  );
}

export default App;
