import ErrorBoundary from "./app/ErrorBoundary";
import AppProviders from "./app/AppProviders";
import Router from "./app/router";

function App() {

  return (
   
      <ErrorBoundary>
       <AppProviders>
       <Router />
       </AppProviders>
      </ErrorBoundary>
  );     
}

export default App;