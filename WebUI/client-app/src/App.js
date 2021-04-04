import './App.scss';
import AppToast from './components/AppToast';
import AppRoute from './components/AppRoute';
import AppModalContainer from './components/AppModalContainer';

const App = () => {
  return (
    <div className="App">
      <AppToast />
      <AppModalContainer />
      <main>
        <AppRoute />
      </main>
    </div>
  );
}

export default App;
