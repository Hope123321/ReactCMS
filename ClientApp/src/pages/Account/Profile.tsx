import * as React from "react";
import Box from "@mui/material/Box";
import {
  Alert,
  AlertColor,
  Button,
  Divider,
  Grid,
  Snackbar,
  Stack,
  TextField,
  Typography,
} from "@mui/material";
import { BasicResponse } from "../../types/_common/basic/http";
import { GetByAuth, PostByAuth } from "../../services/APIService";
import { GetProfileResponse } from "../../types/Member/GetProfile";
import UserContext from "../../contexts/UserContext";
import { UpdateProfileRequest, UpdateProfileResponse } from "../../types/Member/UpdateProfile";
import { MobileDatePicker } from "@mui/x-date-pickers/MobileDatePicker";
import { LocalizationProvider } from "@mui/x-date-pickers";
// or for Day.js
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';

export function Profile() {

  const userContext:any = React.useContext(UserContext);
  const [SaveMessage, setSaveMessage] = React.useState<MessageState | null>(
    null
  );

  const handleClose = (
    event?: React.SyntheticEvent | Event,
    reason?: string
  ) => {
    if (reason === "clickaway") {
      return;
    }

    setSaveMessage(null);
  };

  // 第一次載入執行
  React.useEffect(() => {
    console.log("First Amount!");
    ProfileInit();
  }, []);

  React.useEffect(() => {
    console.log("SaveMessage Change!");
    console.log(SaveMessage);
  }, [SaveMessage]);

  // #region 表單資料綁定State
  //姓名
  const [UserNa, setUserNa] = React.useState("");
  //手機
  const [Mobile, setMobile] = React.useState("");
  //生日
  const [BRDate, setBRDate] = React.useState("");
  // #endregion

  // #region 控制項事件
  const txtUserNa_onChange = (event: any) => {
    setUserNa(event.target.value);
  };

  const txtMobile_onChange = (event: any) => {
    setMobile(event.target.value);
  };
  // #endregion

  // #region 內部方法
  //
  const ProfileInit = async () => {
    try {
      // #region API作業
      let res: BasicResponse = await GetByAuth("/API/Member/GetProfile");
      console.log(res);
      if (res.ResponseNo == "0000") {
        const resData= res.ResponseData as GetProfileResponse;

        if(resData.UserNa)
          setUserNa(resData.UserNa);

        if(resData.Mobile)
          setMobile(resData.Mobile);
        
      } else {
        setSaveMessage({
          type: "warning",
          message: res.ResponseNa,
        });
      }
      // #endregion
    } catch (ex) {
      console.error(ex);
      setSaveMessage({
        type: "error",
        message: `發生異常`,
      });
    }
  };
  //會員儲存
  const SaveMember = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    const data = new FormData(event.currentTarget);
    let values: UpdateProfileRequest = {
      UserNo: userContext[0].UserNo as string ,
      UserNa: data.get("UserNa") as string,//UserNa
      Mobile: data.get("Mobile") as string//Mobile
    };
    console.log(values);

    let message: MessageState | null = null;
    try {
      // #region API作業
      let res: BasicResponse = await PostByAuth("/API/Member/UpdateProfile",values);
      if (res.ResponseNo == "0000") {
        const resData = res.ResponseData as UpdateProfileResponse;
        if (resData.UpdateNo=="00") {
          message = {
            type: "success",
            message: "資料修改成功!",
          };
        }else {
        message = {
          type: "warning",
          message: resData.UpdateNa,
        };
      }
      } else {
        message = {
          type: "warning",
          message: res.ResponseNa,
        };
      }
      // #endregion
    } catch (ex) {
      message = {
        type: "error",
        message: `發生異常`,
      };
    }
    setSaveMessage(message);
  };
  // #endregion

  return (
    <>
      {/* 頭像 */}
      <Box sx={{ m:1,p: 2, border: '1px dashed grey' }}>頭像區</Box>
      {/* 頭像 */}
      <Box sx={{ m:1,p: 2, border: '1px dashed grey' }}>頭像區</Box>
      {/* 個人資料編輯 */}
      <Box sx={{ m:1,p: 2, border: '1px dashed grey' }}>
        <Box m={1}>
          <Typography>個人資料編輯區</Typography>
        </Box>
        <Box component="form" noValidate onSubmit={SaveMember} sx={{ mt: 1 }}>
          <Stack direction="row" m={1} spacing={1}>
            <Button variant="contained" type="submit">
              儲存
            </Button>
            <Button variant="contained" onClick={ProfileInit}>重設</Button>
          </Stack>
          <Box sx={{ m:1,p: 2 }} >
            <Grid container spacing={{ xs: 2, md: 3 }} columns={{ xs: 4, sm: 8, md: 12 }} sx={{ m:1,p: 2 }}>
              <Grid xs={4} sm={4} md={4}>
                <TextField
                  error={false}
                  // margin="normal"
                  required
                  id="txtUserNa"
                  label=" 會員姓名"
                  name="UserNa"
                  value={UserNa}
                  onChange={txtUserNa_onChange}
                  autoFocus
                />
              </Grid>
              <Grid xs={4} sm={4} md={4}>
                <TextField
                  error={false}
                  //margin="normal"
                  required
                  id="txtMobile"
                  label=" 會員電話"
                  name="Mobile"
                  value={Mobile}
                  onChange={txtMobile_onChange}
                />
              </Grid>
                
              <Grid xs={4} sm={4} md={4}>
              <LocalizationProvider dateAdapter={AdapterDayjs}>
                  <MobileDatePicker
                    label="會員生日"
                    inputFormat="YYYY/MM/DD"
                    
                    value={BRDate}
                    onChange={(newValue:string|null)=>{setBRDate(newValue as string)}}
                    renderInput={(params) => <TextField {...params} />}
                  />
                  </LocalizationProvider>
              </Grid>
               
                <Grid xs={4} sm={4} md={4}>
                  <Typography>個人資料編輯區</Typography>
                </Grid>
            </Grid>
            </Box>
        </Box>
        <Snackbar
          open={SaveMessage ? true : false}
          autoHideDuration={5000}
          onClose={handleClose}
        >
          <Alert
            onClose={handleClose}
            severity={SaveMessage ? (SaveMessage as MessageState).type : "info"}
            sx={{ width: "100%" }}
          >
            {SaveMessage ? (SaveMessage as MessageState).message : ""}
          </Alert>
        </Snackbar>
      </Box>
      {/* 最近操作紀錄 */}
      <Box sx={{ m:1,p: 2, border: '1px dashed grey' }}>
        最近操作紀錄
      </Box>
    </>
  );
}

interface MessageState {
  type: AlertColor;
  message: string;
}
