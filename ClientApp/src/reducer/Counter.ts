
//初始state
const initState = {
    count: 0
};

// #region 定義Action類別
//增加
const INCREMENT = 'INCREMENT';
//減少
const DECREMENT = 'DECREMENT';
// #endregion 

// #region 匯出Action
//增加
export const countPlus = { type: INCREMENT }
//減少
export const countMinus = { type: DECREMENT }
// #endregion

// #region reducer本體
const counterReducer = (state = initState, action: any) => {
    //依照action的類型作判斷
    switch (action.type) {
        case INCREMENT:
            return { count: state.count + 1 };
        case DECREMENT:
            return { count: state.count - 1 };
        default:
            return state;
    }
};
// #endergion 

export default counterReducer;