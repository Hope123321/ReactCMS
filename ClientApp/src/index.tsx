import 'bootstrap/dist/css/bootstrap.css';

import * as React from 'react';
//import * as ReactDOM from 'react-dom';
import { Provider } from 'react-redux';
//import { ConnectedRouter } from 'connected-react-router';
//import { createBrowserHistory } from 'history';
//import configureStore from './store/configureStore';
import App from './App';
import registerServiceWorker from './registerServiceWorker';
import store from './store/configureStore';
import { createRoot } from 'react-dom/client';

// Create browser history to use in the Redux store
//const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href') as string;
//const history = createBrowserHistory({ basename: baseUrl });

// Get the application-wide store instance, prepopulating with state from the server where available.
//const store = configureStore(history);

//ReactDOM.render(
//    <Provider store={store}>
//        {/*<ConnectedRouter history={history}>*/}
//        <App />
//        {/*</ConnectedRouter>*/}
//    </Provider>
//    ,
//    document.getElementById('root'));

//registerServiceWorker();

const container = document.getElementById('root');
const root = createRoot(container!); // createRoot(container!) if you use TypeScript
root.render(
    <Provider store={store}>
        //{/*<ConnectedRouter history={history}>*/}
        <App />
        //{/*</ConnectedRouter>*/}
    </Provider>
);
registerServiceWorker();