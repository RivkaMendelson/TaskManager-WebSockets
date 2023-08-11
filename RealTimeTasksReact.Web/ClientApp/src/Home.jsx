import React, { useState, useEffect, useRef } from 'react';
import { HubConnectionBuilder } from '@microsoft/signalr';
import { useAuthDataContext } from './AuthContext';


const Home = () => {

    const { user } = useAuthDataContext();

    const [task, setTask] = useState('');
    const [allTasks, setAllTasks] = useState([]);

    const connectionRef = useRef(null);

    useEffect(() => {

        const connectToHub = async () => {
            const connection = new HubConnectionBuilder().withUrl("/api/test").build();
            await connection.start();
            connection.invoke('LoadTasks');
            connectionRef.current = connection;


            connection.on('renderTasks', tasks => {
                setAllTasks(tasks);
            });

            connection.on('newTaskReceived', newTask => {
                setAllTasks(allTasks => [...allTasks, newTask]);
            });

        }

        connectToHub();

    }, []);

    const onAddTaskClick = async () => {
        await connectionRef.current.invoke('NewTask', task);
        setTask("");
    }

    const takeTaskClicked = (taskId) => {
        connectionRef.current.invoke('TakeTask', taskId);
    }

    const taskDoneClicked = (taskId) => {
        connectionRef.current.invoke('TaskDone', taskId);
    }

    const getButton = (task) => {
        if (task.userId === user.id) {
            return (<button className="btn btn-dark" onClick={()=>taskDoneClicked(task.id) }> I'm Done:) </button>)
        }
        if (task.user) {
            return (<button className="btn btn-warning" disabled>
                {task.user} is doing this one.
            </button>)
        }
        return (<button className="btn btn-primary" onClick={() => takeTaskClicked(task.id)} >
            I'll take it!
        </button>)
    }

    return (<>

        <div className="container" style={{ marginTop: 80 }}>
            <div style={{ marginTop: 70 }}>
                <div className="row">

                    <div className="col-md-10">
                        <input type="text" className="form-control" onChange={e => setTask(e.target.value)} placeholder="Task Title" value={task} />
                    </div>

                    <div className="col-md-2">
                        <button className="btn btn-primary w-100" onClick={onAddTaskClick} disabled={!task}>Add Task</button>
                    </div>

                </div>

                <table className="table table-hover table-striped table-bordered mt-3">
                    <thead>
                        <tr>
                            <th>Title</th>
                            <th>Status</th>
                        </tr>
                    </thead>
                    <tbody>
                        {allTasks.map(t => {
                            return (
                                <tr key={t.id }>
                                    <td>{t.task}</td>
                                    <td>{getButton(t)}</td>
                                </tr>)
                        })}

                    </tbody>
                </table>
            </div>
        </div>
    </>)


};

export default Home;