## ListView入门
###1.在布局中添加ListView
```
<ListView
        android:id="@+id/lv"
        android:layout_width="match_parent"
        android:layout_height="match_parent" />
```
>宽高应该设置为match_parente而不是wrap_content，否则将调用更多次getView()
### 2.定义列表适配器，继承BaseAdapter
```
private class MyListAdapter extends BaseAdapter {

    //决定要显示几个item
    @Override
    public int getCount() {return 100;}
    //不用重写
    @Override
    public Object getItem(int position) {return null;}
    //不用重写
    @Override
    public long getItemId(int position) {return 0;}
    //设置每一个item是什么view，当条目显示时被调用
    @Override
    public View getView(int position, View convertView, ViewGroup parent) {
        TextView tv = new TextView(getApplicationContext());
        tv.setText("哈哈"+position);
        return tv;
    }
}
```
>使用缓存,convertView表示划出屏幕的那个item
```
@Override
        public View getView(int position, View convertView, ViewGroup parent) {
            TextView tv = null;
            if (convertView == null) {
                tv = new TextView(getApplicationContext());
            } else {
                tv= (TextView) convertView;
            }
            tv.setText("哈哈"+position);
            System.out.println("哈哈"+position);
            return tv;
        }
```
### 3.指定适配器
```
ListView lv = (ListView)findViewById(R.id.lv);
lv.setAdapter(new MyListAdapter());
```
## ListView展示数据的原理
和javaweb类似采用MVC,Adapter(C)把JavaBean(M)的数据展示到View(V)上