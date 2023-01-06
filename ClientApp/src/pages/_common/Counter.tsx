import { Divider } from '@mui/material';
import * as React from 'react';
import { useDispatch } from 'react-redux';
import {  useSelector } from 'react-redux';
import { countMinus, countPlus } from '../../reducer/Counter';


export function Counter() {
    //1.使用Redux方式
    //透過useDispatch去呼叫Reducer
    const dispatch = useDispatch();
    //透過useSelector去取得State
    const count: number = useSelector(state => (state as any).counterReducer.count);

    //2.透過useState方式
    const [ Count2, setCount2 ] = React.useState(0);

    return (
        <React.Fragment>
            <h1>Counter</h1>

            <p>This is a simple example of a React Hook.</p>

            <h3> Redux</h3>
            <p aria-live="polite">Current count: <strong>{count}</strong></p>

            <button style={{ margin: '5px' }}
                type="button"
                className="btn btn-primary btn-lg"
                onClick={() => { dispatch(countPlus); }}>
                Increment
            </button>
            <button style={{ margin: '5px' }}
                type="button"
                className="btn btn-primary btn-lg"
                onClick={() => { dispatch(countMinus); }}>
                Decrement
            </button>

            <Divider />

            <h3> useState</h3>
            <p aria-live="polite">Current count: <strong>{Count2}</strong></p>

            <button style={{ margin: '5px' }}
                type="button"
                className="btn btn-primary btn-lg"
                onClick={() => { setCount2(cnt => cnt+1); }}>
                Increment
            </button>
            <button style={{ margin: '5px' }}
                type="button"
                className="btn btn-primary btn-lg"
                onClick={() => { setCount2(cnt => cnt - 1); }}>
                Decrement
            </button>
        </React.Fragment>
    );
}

