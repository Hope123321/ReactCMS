import { Button, Dialog, DialogActions, DialogContent, DialogContentText, DialogTitle } from '@mui/material';
import * as React from 'react';
import { useContext } from 'react';
import { isCallExpression } from 'typescript';
import UserContext from '../../contexts/UserContext';

export default function AlertDialog(
    { message
        ,caption
        ,Open
        ,SetOpen
        ,IsCancel
        ,ConfirmWord
        ,CancelWord
        ,ClickConfirm
        ,ClickCancel
     }: AlertDialogProps) {

    const userContext:any = useContext(UserContext)
    
    const handleClose = () => {
        SetOpen(false);
    };

    function BaseClickConfirm(){
        if(ClickConfirm){
            ClickConfirm();
        }
        handleClose();
    }

    function BaseClickCancel(){
        if(ClickCancel){
            ClickCancel();
        }
        handleClose();
    }

    return (
          <Dialog
            open={Open}
            onClose={handleClose}
            aria-labelledby="alert-dialog-title"
            aria-describedby="alert-dialog-description"
          >
            <DialogTitle id="alert-dialog-title">
            {message}
            </DialogTitle>
            <DialogContent>
              <DialogContentText id="alert-dialog-description">
              {caption}
                {/* Let Google help apps determine location. This means sending anonymous
                location data to Google, even when no apps are running. */}
              </DialogContentText>
            </DialogContent>
            <DialogActions>
              <Button variant="outlined" onClick={BaseClickCancel} hidden={!IsCancel}>
                {CancelWord?CancelWord:"取消"}
                </Button>
              <Button variant="contained" onClick={BaseClickConfirm} autoFocus>
                {ConfirmWord?ConfirmWord:"確定"}
              </Button>
            </DialogActions>
          </Dialog>
      );
}
interface AlertDialogProps{
    message:string
    caption:string
    Open:boolean
    SetOpen:Function
    IsCancel?:boolean
    ConfirmWord?:string
    ClickConfirm?:Function
    CancelWord?:string
    ClickCancel?:Function
}