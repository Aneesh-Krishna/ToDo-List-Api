import React, { useEffect, useState } from 'react';
import TaskList from './components/TaskList';
import './App.css'; // Import the CSS file for custom styles

function App() {
  const [tasks, setTasks] = useState([]);
  const [newTask, setNewTask] = useState('');
  const API_URL = 'https://localhost:7226/api/task'; // Replace with your actual API URL

  // Fetch tasks from the backend on component mount
  useEffect(() => {
    const fetchTasks = async () => {
      try {
        const response = await fetch(API_URL, {
          method: 'GET',
          headers: { 'Content-Type': 'application/json' },
        });
        const data = await response.json();
        setTasks(data);
      } catch (error) {
        console.error('Error fetching tasks:', error);
      }
    };
    fetchTasks();
  }, []);

  // Add a new task
  const addTask = async () => {
    if (newTask.trim()) {
      try {
        const response = await fetch(API_URL, {
          method: 'POST',
          headers: { 'Content-Type': 'application/json' },
          body: JSON.stringify({ taskName: newTask, isCompleted: false }),
        });
        const newTaskFromServer = await response.json();
        setTasks((prevTasks) => [...prevTasks, newTaskFromServer]);
        setNewTask('');
      } catch (error) {
        console.error('Error adding task:', error);
      }
    }
  };

  // Toggle task completion
  const toggleComplete = async (id) => {
    try {
      const response = await fetch(`${API_URL}/${id}`, {
        method: 'PUT',
      });
      if (response.ok) {
        setTasks((prevTasks) =>
          prevTasks.map((task) =>
            task.toDoTaskId === id
              ? { ...task, isCompleted: !task.isCompleted }
              : task
          )
        );
      }
    } catch (error) {
      console.error('Error toggling task:', error);
    }
  };

  // Delete a task
  const deleteTask = async (id) => {
    try {
      const response = await fetch(`${API_URL}/${id}`, {
        method: 'DELETE',
      });
      if (response.ok) {
        setTasks((prevTasks) => prevTasks.filter((task) => task.toDoTaskId !== id));
      }
    } catch (error) {
      console.error('Error deleting task:', error);
    }
  };

  return (
    <div className="app-container">
      <div className="todo-container">
        <h1>ToDo-List App</h1>
        <TaskList
          tasks={tasks}
          toggleComplete={toggleComplete}
          deleteTask={deleteTask}
        />
        <div className="input-container">
          <input
            type="text"
            value={newTask}
            onChange={(e) => setNewTask(e.target.value)}
            placeholder="Enter a new task"
          />
          <button className="btn btn-primary" onClick={addTask}>
            Add Task
          </button>
        </div>
      </div>
    </div>
  );
}

export default App;
