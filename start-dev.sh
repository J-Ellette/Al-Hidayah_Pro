#!/bin/bash

# Start Al-Hidayah Pro Development Environment
# This script starts both the backend API and frontend dev server

echo "=========================================="
echo "  Starting Al-Hidayah Pro Development"
echo "=========================================="
echo ""

# Function to cleanup on exit
cleanup() {
    echo ""
    echo "Shutting down servers..."
    kill $BACKEND_PID $FRONTEND_PID 2>/dev/null
    exit 0
}

# Set up trap to cleanup on script exit
trap cleanup SIGINT SIGTERM EXIT

# Start the backend API
echo "Starting backend API server..."
cd backend/AlHidayahPro.Api
dotnet run --urls http://localhost:5000 > ../../backend.log 2>&1 &
BACKEND_PID=$!
cd ../..

# Wait for backend to start
echo "Waiting for backend to initialize..."
sleep 5

# Check if backend is running
if ! curl -s http://localhost:5000 > /dev/null 2>&1; then
    echo "Warning: Backend may not have started properly. Check backend.log for details."
fi

echo "✓ Backend API is running on http://localhost:5000"
echo ""

# Start the frontend dev server
echo "Starting frontend dev server..."
npm run dev > frontend.log 2>&1 &
FRONTEND_PID=$!

# Wait for frontend to start
sleep 3

echo "✓ Frontend is running on http://localhost:5173"
echo ""
echo "=========================================="
echo "  Al-Hidayah Pro is ready!"
echo "=========================================="
echo ""
echo "Backend API: http://localhost:5000"
echo "Frontend:    http://localhost:5173"
echo ""
echo "API Endpoints:"
echo "  - Quran:  http://localhost:5000/api/quran/surahs"
echo "  - Hadith: http://localhost:5000/api/hadith/collections"
echo "  - Audio:  http://localhost:5000/api/audio/reciters"
echo ""
echo "SignalR Hub: ws://localhost:5000/hubs/study"
echo ""
echo "Press Ctrl+C to stop all servers"
echo ""

# Wait indefinitely
wait
