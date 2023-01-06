import { createStore, combineReducers } from 'redux';
import counterReducer  from '../reducer/Counter';

const rootReducer = combineReducers({
    counterReducer,
});

const store = createStore(rootReducer);

export default store;