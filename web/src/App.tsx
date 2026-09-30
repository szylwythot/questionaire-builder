import { useEffect, useState } from 'react'
import { Button, Container, CssBaseline, Typography } from '@mui/material'

function App() {
  const [status, setStatus] = useState('checking...')

  useEffect(() => {
    fetch('/api/health')
      .then((res) => res.json())
      .then((data) => setStatus(`API OK, ${data.questionnaires} questionnaires`))
      .catch(() => setStatus('API not reachable'))
  }, [])

  return (
    <>
      <CssBaseline />
      <Container sx={{ mt: 4 }}>
        <Typography variant="h4">Questionnaire Builder</Typography>
        <Typography sx={{ my: 2 }}>{status}</Typography>
        <Button variant="contained">MUI works</Button>
      </Container>
    </>
  )
}

export default App
