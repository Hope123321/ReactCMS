import * as React from 'react';
import Avatar from '@mui/material/Avatar';
import Button from '@mui/material/Button';
import CssBaseline from '@mui/material/CssBaseline';
import TextField from '@mui/material/TextField';
import FormControlLabel from '@mui/material/FormControlLabel';
import Checkbox from '@mui/material/Checkbox';
import Link from '@mui/material/Link';
import Paper from '@mui/material/Paper';
import Box from '@mui/material/Box';
import Grid from '@mui/material/Grid';
import LockOutlinedIcon from '@mui/icons-material/LockOutlined';
import Typography from '@mui/material/Typography';
import { createTheme, ThemeProvider } from '@mui/material/styles';
import { login } from '../../services/AuthService';
import { BasicResponse } from '../../types/_common/basic/http';
import { useContext, useState } from 'react';
import { User } from '../../types/_common/login/login';
import UserContext from '../../contexts/UserContext';
import Loading from '../../components/Loading/Loading';

function Copyright(props: any) {
    return (
        <Typography variant="body2" color="text.secondary" align="center" {...props}>
            {'Copyright © '}
            <Link color="inherit" href="https://retex.com.tw/">
                Retex
            </Link>{' '}
            {new Date().getFullYear()}
            {'.'}
        </Typography>
    );
}

const theme = createTheme();

export default function Login() {
    //登入訊息
    const [LoginMsg, setLoginMsg] = useState<string>('');
    //登入資訊Context
    const userContext:any = useContext(UserContext)
    //讀取動畫
    const [OpenLoading, SetOpenLoading] = useState<boolean>(false);
    //登入確認
    const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        const data = new FormData(event.currentTarget);
        let values: any = {
            account: data.get('account'),
            password: data.get('password'),
            remember: data.get('remember')?true:false,
        };
        console.log(values);
        //讀取動畫開啟
        SetOpenLoading(true);
        try{
        // #region 登入作業
        let res: BasicResponse = await login(values.account, values.password,values.remember);
        if (res.ResponseNo == '0000') {
            const resData: any = res.ResponseData;
            if (resData.Token) {
                let user: User = { UserNo: resData.UserNo, UserNa: resData.UserNa, Token: resData.Token };
                localStorage.setItem('user', JSON.stringify(user));
                //設定資料
                userContext[1](user);
            }
            else {
                setLoginMsg("登入出現異常，請休息一會再嘗試。");
            }
        } else {
            setLoginMsg(res.ResponseNa);
        }
        // #endregion
        }catch(ex){
            setLoginMsg("登入出現異常，請休息一會再嘗試。");
        }
        //讀取動畫關閉(避免畫面閃一下，最短都暫存兩秒再關掉)
        setTimeout(()=>{
            SetOpenLoading(false);
        },2000);
    };

    return (
        <ThemeProvider theme={theme}>
            <Grid container component="main" sx={{ height: '100vh' }}>
                <CssBaseline />
                <Grid
                    item
                    xs={false}
                    sm={4}
                    md={7}
                    sx={{
                        backgroundImage: 'url(https://source.unsplash.com/random)',
                        backgroundRepeat: 'no-repeat',
                        backgroundColor: (t) =>
                            t.palette.mode === 'light' ? t.palette.grey[50] : t.palette.grey[900],
                        backgroundSize: 'cover',
                        backgroundPosition: 'center',
                    }}
                />
                <Grid item xs={12} sm={8} md={5} component={Paper} elevation={6} square>
                    <Box
                        sx={{
                            my: 8,
                            mx: 4,
                            display: 'flex',
                            flexDirection: 'column',
                            alignItems: 'center',
                        }}
                    >
                        <Avatar sx={{ m: 1, bgcolor: 'secondary.main' }}>
                            <LockOutlinedIcon />
                        </Avatar>
                        <Typography component="h1" variant="h5">
                            Sign in
                        </Typography>
                        <Box component="form" noValidate onSubmit={handleSubmit} sx={{ mt: 1 }}>
                            <TextField
                                error={LoginMsg !== ''}
                                margin="normal"
                                required
                                fullWidth
                                id="account"
                                label="Account"
                                name="account"
                                autoFocus
                            />
                            <TextField
                                error={LoginMsg !== ''}
                                margin="normal"
                                required
                                fullWidth
                                name="password"
                                label="Password"
                                type="password"
                                id="password"
                                autoComplete="current-password"
                                helperText={LoginMsg !== '' ? LoginMsg:""}
                            />
                            <FormControlLabel
                                control={<Checkbox  value="remember" color="primary" />}
                                label="Remember me"
                                name="remember"
                            />
                            <Button
                                type="submit"
                                fullWidth
                                variant="contained"
                                sx={{ mt: 3, mb: 2 }}
                            >
                                Sign In
                            </Button>
                            <Grid container>
                                <Grid item xs>
                                    <Link href="#" variant="body2">
                                        Forgot password?
                                    </Link>
                                </Grid>
                                {/* <Grid item>
                                    <Link href="#" variant="body2">
                                        {"Don't have an account? Sign Up"}
                                    </Link>
                                </Grid> */}
                            </Grid>
                            <Copyright sx={{ mt: 5 }} />
                        </Box>
                    </Box>
                </Grid>
            </Grid>
            {/* 讀取動畫初始化 */}
            <Loading Open={OpenLoading} setOpen={SetOpenLoading}/>
        </ThemeProvider>
    );
}